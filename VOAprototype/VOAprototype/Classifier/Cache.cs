using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOAprototype.Models;

namespace VOAprototype.Classifier
{
    public static class Cache
    {
        private static readonly VOAprototypeContext _context;

        private static readonly ScriptEngine Engine = Python.CreateEngine();
        private static readonly ScriptSource Source = Engine.CreateScriptSourceFromFile(@"./Classifier/bigrams2_wesley.py");
        private static readonly ScriptScope Scope = Engine.CreateScope();

        public static void Init()
        {
            /*ICollection<string> searchPaths = Engine.GetSearchPaths();
            searchPaths.Add("./Lib");
            searchPaths.Add("./Lib/site-packages");
            searchPaths.Add(Environment.CurrentDirectory);
            Engine.SetSearchPaths(searchPaths);
            Source.Execute(Scope);
            Train();*/
        }

        public static void Train()
        {
            var itfuncs = from itf in _context.ITFunction select itf;

            foreach (ITFunction itfunction in itfuncs)
            {

            }
        }
    }
}
