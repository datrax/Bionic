using System;
using System.Collections.Generic;


namespace SimpleWEBServer
{
    /// <summary>
    /// Class that creates biis objects, it's compiled and executed at runtime
    /// </summary>
    public class ServiceClass
    {

        private interface IComponents
        {
            string ReturnHTMLCode();
        }

        private class Label : IComponents
        {
            public string name { get; set; } 

            public string value { get; set; } 

            public Label(string name, string value)
            {
                this.name = name;
                this.value = value;
            }

            public string ReturnHTMLCode()
            {
                return String.Format("<label name = \"{0}\">{1}</label>",name,value);
            }
        }

        private class Button : IComponents
        {
            public string name { get; set; } 

            public string value { get; set; } 

            public Button(string name, string value)
            {
                this.name = name;
                this.value = value;
            }

            public string ReturnHTMLCode()
            {
                return String.Format("<input type=\"submit\" name = \"{0}\" value=\"{1}\" formmethod=\"post\"/>", name,value); ;
            }
        }
        List<IComponents> list = new List<IComponents>();

    }
}
