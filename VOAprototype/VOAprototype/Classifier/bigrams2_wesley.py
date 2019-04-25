#import packages associated with wikipedia scraping and data manipulation
import wikipedia
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
from sklearn import model_selection, neural_network
from sklearn.neural_network import MLPClassifier
from nltk.util import ngrams

class Classifier(object):
    stop_words = set(stopwords.words('english'))
    
    scraped = pd.DataFrame()
    
    Tfidf_vect = TfidfVectorizer(max_features=5000)
    tfidf_data = []
    Train_X_Tfidf = []
    Train_Y = []

    def detokenize(self, tokens):
        return "".join([" "+i if not i.startswith("'") and i not in string.punctuation else i for i in tokens]).strip()
        
    
    def tokenizeAndfilter(self, content):
        word_tokens = word_tokenize(content) 
        word_tokens= [word.lower() for word in word_tokens if word.isalpha()]
        filtered_sentence = [w for w in word_tokens if not w in self.stop_words] 
        for w in word_tokens: 
            if w not in self.stop_words: 
                filtered_sentence.append(w) 
        filtered_sentence = self.detokenize(filtered_sentence, return_str=True)
        joined_n_grams = []
        for index, entry in enumerate(filtered_sentence):
            n_grams = ngrams(word_tokenize(entry), 2)
            for grams in n_grams:
               joined_n_grams.append(str(' '.join(grams)))
        return joined_n_grams
        
    
    def mapITFunction(self, itfunc, wikiPageTitle):
        search = wikipedia.page(wikiPageTitle)
        if search != []:
            self.scraped['itfunction'].append(itfunc)
            self.scraped['tokens'].append(self.tokenizeAndfilter(search.content))
                
    def Train(self):
        self.Tfidf_vect.fit(self.scraped['tokens'])
        Train_X, Test_X, Train_Y, Test_Y = model_selection.train_test_split(self.scraped['itfunction'],self.scraped['tokens'],test_size=0)

        Encoder = LabelEncoder()
        self.Train_Y = Encoder.fit_transform(Train_Y)
        OLD_Y = Encoder.inverse_transform(Train_Y)
        
        self.tfidf_data = pd.DataFrame({'Train_Y': Train_Y,'OLD_Y': OLD_Y})
        
        self.Train_X_Tfidf = Tfidf_vect.transform(Train_X)
        
    def ClassifyThree(self, itfunc, description):
        classified = list()
        desc_tokens = self.tokenizeAndfilter(description)
        transform_Tfidf = self.Tfidf_vect.transform(desc_tokens)
        
        ## neural network
        nn = MLPClassifier(alpha=1)
        nn.fit(self.Train_X_Tfidf, self.Train_Y)
        predProbs = pd.DataFrame(nn.predict_proba(transform_Tfidf), columns=nn.classes_)
        predictions = predProbs.apply(lambda row: row.sort_values(ascending=False).index, axis=1)
        classified.append("Hello")
        return classified