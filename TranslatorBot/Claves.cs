using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorBot
{
    //Reads and returns the keys 

    public class Claves
    {
        public string botToken
        { get; private set; }
        public string translatorKey
        { get; private set; }
        public string translatorEndopoint
        { get; private set; }
         
        public Claves()
        {
            botToken = File.ReadAllText(@"SecretBotToken.txt");
            translatorKey = File.ReadAllText(@"Key.txt");
            translatorEndopoint = "https://api.cognitive.microsofttranslator.com/";
        }
    }
}
