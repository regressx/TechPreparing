namespace NavisElectronics.TechPreparation.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Data;
    using Intermech.Commands;
    using Entities;
    using IO;

    /// <summary>
    /// Класс-менеджер для просмотра прикрепленных файлов к объекту
    /// </summary>
    public class ShowFileManager
    {
        private IntermechReader _reader;

        public void Show(long id, int type)
        {
            if (type == 1078 || type == 1074)
            {
                if (_reader == null)
                {
                    _reader = new IntermechReader();
                }

                IList<Document> docs = _reader.GetDocuments(id).ToList();
                if (docs.Count > 0)
                {
                    id = docs[0].Id;
                }
                else
                {
                    throw new FileNotFoundException("К указанному объекту нет прикрепленных сборочных чертежей");
                }
            }

            ObjectCommand myCommand = ObjectCommandFactory.CreateViewCommand(false);
            myCommand.ObjectId = id;
            myCommand.Execute();
        }
    }
}