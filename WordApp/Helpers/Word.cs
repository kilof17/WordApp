using System.Collections.Generic;
using wordInt = Microsoft.Office.Interop.Word;
using System.Reflection;//Missing.Value 
using WordApp.Models;


namespace WordApp
{

    public class Word
    {
        public object miss = Missing.Value;
        public wordInt.Application wordApplication = null;
        public wordInt.Document myDoc = null;

        #region RunWord
        public void RunWord(object selectedTemplate)
        {
            wordApplication = new wordInt.Application();
            object readOnly = false;
            object isVisible = false;
            wordApplication.Visible = false;

            myDoc = wordApplication.Documents.Open(ref selectedTemplate, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref isVisible);
            myDoc.Activate();
        }
        #endregion

        #region Close Word
        private void Close()
        {
            try
            {
                myDoc.Close();
                wordApplication.Quit();
            }
            catch { wordApplication.Quit(); }
        } 
        #endregion

        #region Save
        public string SaveDocument( List<WordControl>controls, File template, Format format) // save formats https://docs.microsoft.com/pl-pl/dotnet/api/microsoft.office.interop.word.wdsaveformat?view=word-pia 
        {
            string userNick = System.Web.HttpContext.Current.User.Identity.Name;

            RunWord( template.FilePaht );
            foreach ( wordInt.ContentControl cc in myDoc.ContentControls )
            {
                if ( format.Format_Int != 17 ) // damages template when selected format is pdf
                { cc.Temporary = true; } // after set text/chceck control is removed from ready file

                bool notExist = true; 

                for ( int i = 0; i < controls.Count; i++ )
                {
                    if ( cc.ID == controls[i].Control_ID )
                    {
                        notExist = false;

                        if (controls[i].Type == 1 || controls[i].Type == 3)
                        { cc.Range.Text = controls[i].Text; }

                        else if ( controls[i].Type == 8 ) // checkbox
                        { cc.Checked = controls[i].Checked; }
                        break; // if find control and change value break loop
                    }
                }
                if (format.Format_Int != 17)
                {
                    if (notExist && cc.Type != 0)// Deleting controls not displayed in form . Type == 0 - marked text
                    { cc.Delete(true); }
                }
            }

            string fileName = template.FileName + "-" + userNick;
            object TargetDoc = System.Web.HttpContext.Current.Server.MapPath("~/TargetDOCS/") + fileName;
            System.IO.File.Delete((string)TargetDoc);// delete prvious version          
            myDoc.SaveAs2(ref TargetDoc, format.Format_Int);// saving new version of file
            Close();
           
            return HashingFunctions.InfoHash(fileName+format.Format_String);
        }
        #endregion

        #region Get controls from template       
        public List<WordControl> GetContorlsFromTemplate(object templatePath)
        {
            List<WordControl> controls = new List<WordControl>();

            RunWord(templatePath); // may demand type casting (object)file.FilePath
            foreach (wordInt.ContentControl cc in myDoc.ContentControls)
            {
                if (cc.Type != 0 && cc.Title != null)// is not the selected text
                {
                    WordControl control = new WordControl()
                    {
                        Control_ID = cc.ID,   
                        Title = cc.Title,
                        Tag = cc.Tag,
                        Type = (int)cc.Type,
                        Text = "",
                        Multiline = cc.MultiLine,
                        Checked = false
                    };

                    if (control.Tag == null)
                    { control.Tag = "empty"; }  //tag can not be empty, becaouse it is couses errors
                    
                    if ((int)cc.Type == 3 || (int)cc.Type == 4)
                    {
                        control.Dropdown = new List<string>();

                        for (int i = 2; i <= cc.DropdownListEntries.Count; i++)  // index counted from 1. Start from 2 to skip tag "Select item"
                        { control.Dropdown.Add(cc.DropdownListEntries[i].Text.ToString()); }                      
                    }
                    controls.Add( control );
                }
            }
            Close();
            return controls;           
        }
        #endregion
    }
}
 
