from flask import Flask
from flask import request
import wikipedia
import string
import numpy
import pandas as pd
#import packages related to natural language processing
import nltk
nltk.download('averaged_perceptron_tagger')
nltk.download('punkt')
nltk.download('wordnet')
nltk.download('stopwords')
nltk.download('perluniprops')
from nltk.tokenize import word_tokenize
from nltk.corpus import stopwords
from sklearn.preprocessing import LabelEncoder
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn import model_selection, naive_bayes
from nltk.util import ngrams
from apscheduler.schedulers.background import BackgroundScheduler

app = Flask(__name__)

lastcall = 1

stop_words = set(stopwords.words('english'))

scraped = pd.DataFrame()
itfunctions = []
filtereddescs = []

tfidf_vect = TfidfVectorizer(max_features=5000, ngram_range=(1, 1))
tfidf_data = []
Train_X_Tfidf = []
Train_Y = []
OLD_Y = []

def detokenize(tokens):
    return "".join([" "+i if not i.startswith("'") and i not in string.punctuation else i for i in tokens]).strip()

def filter_sentence(content):
    word_tokens = word_tokenize(content) 
    word_tokens= [word.lower() for word in word_tokens if word.isalpha()]
    filtered_sentence = [w for w in word_tokens if not w in stop_words]
    filtered_sentence = detokenize(filtered_sentence)
    return filtered_sentence

@app.route("/")
def home():
    global lastcall
    lastcall = lastcall + 1
    return str("Hello World " + str(lastcall))

@app.route("/reset")
def reset():
    global scraped
    global itfunctions
    global filtereddescs
    global tfidf_vect
    global Train_X_Tfidf
    global Train_Y
    global OLD_Y
    scraped = pd.DataFrame()
    itfunctions = []
    filtereddescs = []
    tfidf_vect = TfidfVectorizer(max_features=5000, ngram_range=(1, 1))
    tfidf_data = []
    Train_X_Tfidf = []
    Train_Y = []
    OLD_Y = []
    return "reset"

@app.route("/wikipedia", methods=["POST"])
def wikithis():
    try:
        wikipage = str(request.data, "utf-8")
        search = wikipedia.page(wikipage)
    except Exception as e:
        return str(e)
    else:
        if search != []:
            return search.content
        return "no wiki"

@app.route("/delete/<itfunc>", methods=["DELETE"])
def delete(itfunc):
    try:
        index = itfunctions.index(itfunc.replace("_", " "))
        del itfunctions[index]
        del filtereddescs[index]
    except Exception as e:
        return str(e)
    else:
        return "Deleted"

@app.route("/filter", methods=["POST"])
def nostop():
    try:
        description = str(request.data, "utf-8")
    except Exception as e:
        return str(e)
    else:
        return filter_sentence(description)

@app.route("/learn/<itfunc>", methods=["POST"])
def learn(itfunc):
    description = str(request.data, "utf-8")
    global itfunctions
    global filtereddescs
    try:
        index = itfunctions.index(itfunc.replace("_", " "))
    except:
        return "NOTHING TO LEARN"
    else:
        filtereddescs[index] = filtereddescs[index] + " " + filter_sentence(description)
        return "LEARNED"

@app.route("/map/<itfunc>", methods=["POST"])
def add(itfunc):
    try:
        description = str(request.data, "utf-8")
        global itfunctions
        global filtereddescs
        itfunctions.append(itfunc.replace("_", " "))
        filtereddescs.append(filter_sentence(description))
    except Exception as e:
        return str(e)
    else:
        return "Success"

@app.route("/train")
def train():
    global Train_Y
    global Train_X_Tfidf
    global tfidf_data
    global OLD_Y
    try:
        scrape = pd.Series(filtereddescs)
        scrapeitf = pd.Series(itfunctions)
        tfidf_vect.fit(scrape)
        Train_X, Test_X, Train_Y, Test_Y = model_selection.train_test_split(scrape, scrapeitf, test_size=0)

        Encoder = LabelEncoder()
        Train_Y = Encoder.fit_transform(Train_Y)
        OLD_Y = Encoder.inverse_transform(Train_Y)
        
        tfidf_data = pd.DataFrame({'Train_Y': Train_Y,'OLD_Y': OLD_Y})
        Train_X_Tfidf = tfidf_vect.transform(Train_X)
    except Exception as e:
        return str(e)
    else :
        return "Training"

scheduler = BackgroundScheduler()
scheduler.add_job(func=train, trigger="interval", seconds=60*60)
scheduler.start()

@app.route("/classify/<application>", methods=["POST"])
def classify(application):
    try:
        application = application.replace('_', ' ')
        description = str(request.data, "utf-8")
        classified = []
        desc_tokens = filter_sentence(description.lower().replace("_", " "))
        classified.append(desc_tokens)
        transform_Tfidf = tfidf_vect.transform(classified)
        ## naive bayes
        nb = naive_bayes.MultinomialNB()
        nb.fit(Train_X_Tfidf, Train_Y)
        nb_predict = nb.predict(transform_Tfidf)
        predProbs = pd.DataFrame(nb.predict_proba(transform_Tfidf), columns=nb.classes_)
        predictions = predProbs.apply(lambda row: row.sort_values(ascending=False).index, axis=1)
    except Exception as e:
        return str(e)
    else:
        return str(str(OLD_Y[numpy.where(Train_Y==predictions[0][0])]) + ";" + str(OLD_Y[numpy.where(Train_Y==predictions[0][1])]) + ";" + str(OLD_Y[numpy.where(Train_Y==predictions[0][2])]))

@app.route("/diagnostics")
def diagnostic():
    return (str(itfunctions) + "<br><br><br>" + str(Train_Y) + "<br><br><br>" + str(OLD_Y))

@app.route("/test-post", methods=["POST"])
def testpost():
    return request.data