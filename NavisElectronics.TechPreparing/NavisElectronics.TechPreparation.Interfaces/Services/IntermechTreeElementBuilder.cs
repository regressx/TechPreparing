using System;
using Intermech.Interfaces;
using NavisElectronics.TechPreparation.Exceptions;

namespace NavisElectronics.TechPreparation.Interfaces.Services
{
    using Entities;

    /// <summary>
    /// Построитель узла дерева IntermechTreeElement
    /// </summary>
    public class IntermechTreeElementBuilder
    {
        private readonly IntermechTreeElement _element;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntermechTreeElementBuilder"/> class.
        /// </summary>
        public IntermechTreeElementBuilder()
        {
            _element = new IntermechTreeElement();
            _element.StockRate = 1;
        }

        public IntermechTreeElementBuilder SetId(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустой идентификатор версии объекта");
            }

            if (id == DBNull.Value)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустой идентификатор версии объекта");
            }

            _element.Id = Convert.ToInt64(id);

            return this;
        }

        public IntermechTreeElementBuilder SetObjectId(object objectId)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустой идентификатор объекта ObjectId");
            }

            if (objectId == DBNull.Value)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустой идентификатор объекта ObjectId");
            }
            _element.ObjectId = Convert.ToInt64(objectId);
            return this;
        }

        public IntermechTreeElementBuilder SetType(object type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустой идентификатор типа объекта");
            }

            if (type == DBNull.Value)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустой идентификатор типа объекта");
            }
            _element.Type = Convert.ToInt32(type);

            return this;
        }

        public IntermechTreeElementBuilder SetRelationId(object relationId)
        {
            if (relationId == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустой идентификатор связи");
            }

            if (relationId == DBNull.Value)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустой идентификатор связи");
            }
            _element.RelationId = Convert.ToInt64(relationId);
            return this;
        }

        public IntermechTreeElementBuilder SetDesignation(object designation)
        {
            if (designation == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустое обозначение");
            }

            _element.Designation = designation == DBNull.Value ? string.Empty : Convert.ToString(designation);

            return this;
        }

        public IntermechTreeElementBuilder SetName(object name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустую ссылку на наименование");
            }

            _element.Name = name == DBNull.Value ? string.Empty : Convert.ToString(name);

            return this;
        }

        public IntermechTreeElementBuilder SetChangeNumber(object changeNumber)
        {
            if (changeNumber == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустую ссылку на номер изменения");
            }

            _element.ChangeNumber = changeNumber == DBNull.Value ? string.Empty : Convert.ToString(changeNumber);

            return this;
        }

        public IntermechTreeElementBuilder SetFirstUse(object firstUse)
        {
            if (firstUse == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустую ссылку на первичную применяемость");
            }

            _element.FirstUse = firstUse == DBNull.Value ? string.Empty : Convert.ToString(firstUse);

            return this;
        }

        public IntermechTreeElementBuilder SetLetter(object letter)
        {
            if (letter == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустую ссылку на литеру");
            }

            _element.Letter = letter == DBNull.Value ? string.Empty : Convert.ToString(letter);

            return this;
        }


        public IntermechTreeElementBuilder SetAmount(object amount)
        {
            if (amount == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустую ссылку на количество");
            }

            if (_element.RelationId == 0)
            {
                throw new RelationIdNotFoundException("Для правильного определения единиц измерения требуется сначала задать идентификатор связи");
            }

            if (amount == DBNull.Value)
            {
                _element.Amount = 0;
                return this;
            }

            int longMeasureUnitCode = 2806;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBRelation relation = keeper.Session.GetRelation(_element.RelationId);
                IDBAttribute amountAttribute;
                    
                // Если это не связь "Подборной элемент"
                if (relation.RelationType != 1056)
                {
                    amountAttribute = relation.GetAttributeByID(1129);
                }
                else
                {
                    amountAttribute = relation.GetAttributeByID(1473);
                }

                MeasuredValue currentValue = (MeasuredValue)amountAttribute.Value;
                _element.Amount = (float)currentValue.Value;
                MeasureDescriptor measureDescriptor = MeasureHelper.FindDescriptor(currentValue.MeasureID);
                _element.MeasureUnits = measureDescriptor.ShortName;

                // если мы получили единицу измерения в мм, то надо ее перевести в метры
                if (currentValue.MeasureID == longMeasureUnitCode)
                {
                    _element.Amount /= 1000;
                    _element.MeasureUnits = "м";
                }

                if (currentValue.MeasureID == 2612) // если попали на кубический метр, то переведем его в литр
                {
                    _element.Amount *= 1000;
                    _element.MeasureUnits = "л";
                }

            }

            return this;
        }

        public IntermechTreeElementBuilder SetSubstituteGroupNumber(object substituteGroupNumber)
        {
            if (substituteGroupNumber == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустую ссылку на номер группы изменений");
            }

            _element.SubstituteGroupNumber = substituteGroupNumber == DBNull.Value ? 0 : Convert.ToInt32(substituteGroupNumber);

            return this;
        }

        public IntermechTreeElementBuilder SetSubstituteNumberInGroup(object substituteNumberInGroup)
        {
            if (substituteNumberInGroup == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустую ссылку на номер в группе изменений");
            }

            _element.SubstituteNumberInGroup = substituteNumberInGroup == DBNull.Value ? 0 : Convert.ToInt32(substituteNumberInGroup);

            return this;
        }


        public IntermechTreeElementBuilder SetPositionDesignation(object positionDesignation)
        {
            if (positionDesignation == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустую ссылку на позиционное обозначение");
            }

            _element.PositionDesignation = positionDesignation == DBNull.Value ? string.Empty : Convert.ToString(positionDesignation);

            return this;
        }

        public IntermechTreeElementBuilder SetPosition(object position)
        {
            if (position == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустую ссылку на позицию в спецификации");
            }

            _element.Position = position == DBNull.Value ? string.Empty : Convert.ToString(position);

            return this;
        }

        public IntermechTreeElementBuilder SetSupplier(object supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустую ссылку на поставщика");
            }

            _element.Supplier = supplier == DBNull.Value ? string.Empty : Convert.ToString(supplier);

            return this;
        }


        public IntermechTreeElementBuilder SetClass(object className)
        {
            if (className == null)
            {
                throw new ArgumentNullException("Попытка передать построителю объекта узла дерева пустую ссылку на класс изделия");
            }

            _element.Class = className == DBNull.Value ? string.Empty : Convert.ToString(className);

            return this;
        }


        public IntermechTreeElementBuilder SetPartNumber(object partNumber)
        {
            if (partNumber == null)
            {
                throw new ArgumentNullException("partNumber","Попытка передать построителю объекта узла дерева пустую ссылку на PartNumber");
            }

            _element.PartNumber = partNumber == DBNull.Value ? string.Empty : Convert.ToString(partNumber);

            return this;
        }

        public IntermechTreeElementBuilder SetCase(object caseType)
        {
            if (caseType == null)
            {
                throw new ArgumentNullException("caseType","Попытка передать построителю объекта узла дерева пустую ссылку на корпус изделия");
            }

            _element.Case = caseType == DBNull.Value ? string.Empty : Convert.ToString(caseType);

            return this;
        }

        public IntermechTreeElementBuilder SetPcb(object pcbValue)
        {
            if (pcbValue == null)
            {
                throw new ArgumentNullException("caseType","Попытка передать построителю объекта узла дерева пустую ссылку на флаг печатной платы");
            }

            if (pcbValue == DBNull.Value)
            {
                _element.IsPcb = false;
            }
            else
            {
                _element.IsPcb = Convert.ToByte(pcbValue) == 1;

                if (_element.IsPcb)
                {
                    // TODO: чтение тех. задания
                    using (SessionKeeper keeper = new SessionKeeper())
                    {
                        IDBObject currentObject = keeper.Session.GetObject(_element.Id);
                        IDBAttribute techTaskOnPcbAttribute = currentObject.GetAttributeByID(18086);
                        if (techTaskOnPcbAttribute != null)
                        {
                            char[] textBytes = null;
                            IMemoReader memoReader = techTaskOnPcbAttribute as IMemoReader;
                            memoReader.OpenMemo(0);
                            textBytes = memoReader.ReadDataBlock();
                            memoReader.CloseMemo();
                            _element.TechTask = new string(textBytes);
                        }
                    }
                }
            }

            return this;
        }

        public IntermechTreeElementBuilder SetPcbVersion(object pcbVersion)
        {
            if (pcbVersion == null)
            {
                throw new ArgumentNullException("pcbVersion", "Попытка передать построителю объекта узла дерева пустую ссылку на версию печатной платы");
            }

            _element.PcbVersion = pcbVersion == DBNull.Value ? (byte)0 : Convert.ToByte(pcbVersion);


            return this;
        }

        public IntermechTreeElementBuilder SetMountingType(object mountingType)
        {
            if (mountingType == null)
            {
                throw new ArgumentNullException("mountingType", "Попытка передать построителю объекта узла дерева пустую ссылку на тип монтажа компонента");
            }

            _element.MountingType = mountingType == DBNull.Value ? string.Empty : Convert.ToString(mountingType);

            return this;
        }


        public IntermechTreeElementBuilder SetNote(object note)
        {
            if (note == null)
            {
                throw new ArgumentNullException("note", "Попытка передать построителю объекта узла дерева пустую ссылку на примечание");
            }

            _element.Note = note == DBNull.Value ? string.Empty : Convert.ToString(note);

            return this;
        }


        public IntermechTreeElementBuilder SetRelationNote(object relationNote)
        {
            if (relationNote == null)
            {
                throw new ArgumentNullException("note", "Попытка передать построителю объекта узла дерева пустую ссылку на примечание по связи");
            }

            _element.RelationNote = relationNote == DBNull.Value ? string.Empty : Convert.ToString(relationNote);

            return this;
        }


        public IntermechTreeElement SetChangeDocument(object changeDocument)
        {
            if (changeDocument == null)
            {
                throw new ArgumentNullException("changeDocument", "Попытка передать построителю объекта узла дерева пустую ссылку на обозначение извещения");
            }

            long id = changeDocument == DBNull.Value ? 0 : Convert.ToInt64(changeDocument);
            if (id == 0)
            {
                if (_element.Id == 0)
                {
                    throw new ArgumentNullException("Id", "Вам следует сначала задать идентификатор версии объекта");
                }

                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject documentObject = keeper.Session.GetObject(_element.Id);
                    IDBAttribute documentChangeName = documentObject.GetAttributeByID(17921);
                    if (documentChangeName != null)
                    {
                        _element.ChangeDocument = documentChangeName.AsString;
                    }
                }
            }
            else
            {
                using (SessionKeeper keeper = new SessionKeeper())
                {
                    try
                    {
                        IDBObject documentObject = keeper.Session.GetObject(id);
                        _element.ChangeDocument = documentObject.Caption;
                    }
                    catch (Exception)
                    {
                        _element.ChangeDocument = string.Empty;
                    }

                }
            }

            return this;
        }

        /// <summary>
        /// Оператор неявного преобразования в IntermechTreeElement 
        /// </summary>
        /// <param name="builder">
        /// Построитель
        /// </param>
        /// <returns>
        /// Узел IntermechTreeElement
        /// </returns>
        public static implicit operator IntermechTreeElement(IntermechTreeElementBuilder builder)
        {
            return builder._element;
        }


    }
}