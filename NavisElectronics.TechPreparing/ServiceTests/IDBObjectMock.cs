using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Intermech;
using Intermech.Interfaces;
using Intermech.Kernel.Search;

namespace ServiceTests
{
    public class IDBObjectMock:IDBObject
    {
        public IUserSession Session { get; }
        public IDBAttribute GetAttributeByID(int attributeID)
        {
            throw new NotImplementedException();
        }

        public IDBAttribute GetAttributeByGuid(Guid attributeGuid)
        {
            throw new NotImplementedException();
        }

        public IDBAttribute GetAttributeByName(string attributeName)
        {
            throw new NotImplementedException();
        }

        public IDBAttribute GetAttributeByGuid(Guid attributeGuid, bool throwNotFoundException)
        {
            throw new NotImplementedException();
        }

        public IDBAttribute GetAttributeByName(string attributeName, bool throwNotFoundException)
        {
            throw new NotImplementedException();
        }

        public AttributeValues[] GetAttributesValues(GetAttributeValuesModes modes)
        {
            throw new NotImplementedException();
        }

        public AttributeValues[] SetAttributesValues(AttributeValues[] valuesList, bool deleteNotExistingAttributes,
            bool dontDeleteBlobs, bool returnDelta, GetAttributeValuesModes modes)
        {
            throw new NotImplementedException();
        }

        public AttributeValues[] SetAttributesValues(AttributeValues[] valuesList, bool deleteNotExistingAttributes,
            bool dontDeleteBlobs)
        {
            throw new NotImplementedException();
        }

        public AttributeValues[] SetAttributesValues(AttributeValues[] valuesList)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, Exception> SetAttributesValuesEx(AttributeValues[] valuesList, bool deleteNotExistingAttributes, bool dontDeleteBlobs,
            bool returnDelta, GetAttributeValuesModes modes)
        {
            throw new NotImplementedException();
        }

        public object[] GetValuesByGuid(Guid guid, bool throwNotFoundException)
        {
            throw new NotImplementedException();
        }

        public object[] GetValuesByName(string attributeName, bool throwNotFoundException)
        {
            throw new NotImplementedException();
        }

        public object[] GetValuesByID(int attributeID, bool throwNotFoundException)
        {
            throw new NotImplementedException();
        }

        public string[] GetDescriptionsByID(int attributeID, bool throwNotFoundException)
        {
            throw new NotImplementedException();
        }

        public string[] GetDescriptionsByGuid(Guid guid, bool throwNotFoundException)
        {
            throw new NotImplementedException();
        }

        public IDBAttributeType GetAttributeType(int attributeID)
        {
            throw new NotImplementedException();
        }

        public IDBAttribute TryToAddOrDelAttribute(int attrID, object newValue)
        {
            throw new NotImplementedException();
        }

        public AttributeValues[] GetInitAttributesValues(int[] attributeIDs)
        {
            throw new NotImplementedException();
        }

        public AttributeValues[] GetCalculatedValues(AttributeValues[] valuesList, GetAttributeValuesModes modes)
        {
            throw new NotImplementedException();
        }

        public IDBAttributeCollection Attributes { get; }
        public bool ReadOnly { get; }
        public int TypeID { get; }
        public IDBObject CheckOut()
        {
            throw new NotImplementedException();
        }

        public IDBObject CheckOut(bool throwModifyModeException)
        {
            throw new NotImplementedException();
        }

        public int CheckIn()
        {
            throw new NotImplementedException();
        }

        public int CancelChanges(bool isAdminMode)
        {
            throw new NotImplementedException();
        }

        public int CancelChanges()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Edit()
        {
            throw new NotImplementedException();
        }

        public void SaveToArcCopy()
        {
            throw new NotImplementedException();
        }

        public int Delete(long DeleteMode)
        {
            throw new NotImplementedException();
        }

        public void CommitCreation(bool deleteOnException)
        {
            throw new NotImplementedException();
        }

        public void CommitCreation(bool deleteOnException, bool autoCheckout)
        {
            throw new NotImplementedException();
        }

        public string GetHashFile(int versionID, X509Certificate2 certificate, bool setContent, IHashContent hashContent)
        {
            throw new NotImplementedException();
        }

        public int GetHashVersion()
        {
            throw new NotImplementedException();
        }

        public bool isParentType(Guid guid)
        {
            throw new NotImplementedException();
        }

        public DataTable GetLCHistory()
        {
            throw new NotImplementedException();
        }

        public DataTable GetLCHistory(bool allVersions)
        {
            DataTable table = new DataTable();
            DataColumn versionIdColumn = new DataColumn("F_OBJECT_ID", typeof(long));
            DataColumn stepColumn = new DataColumn("F_LC_STEP", typeof(int));
            DataColumn dateColumn = new DataColumn("F_START_DATE", typeof(DateTime));

            table.Columns.AddRange(new DataColumn[] {versionIdColumn,stepColumn,dateColumn});

            // версия на производстве
            DataRow row1 = table.NewRow();
            row1["F_OBJECT_ID"] = 1890;
            row1["F_LC_STEP"] = 1015;
            row1["F_START_DATE"] = new DateTime(19, 02, 13);

            // версия на хранении
            DataRow row2 = table.NewRow();
            row2["F_OBJECT_ID"] = 1890;
            row2["F_LC_STEP"] = 1015;
            row2["F_START_DATE"] = new DateTime(19, 02, 13);

            // вторая версия на производстве
            DataRow row3 = table.NewRow();
            row3["F_OBJECT_ID"] = 1891;
            row3["F_LC_STEP"] = 1015;
            row3["F_START_DATE"] = new DateTime(19, 02, 13);

            table.Rows.Add(row1);
            table.Rows.Add(row2);
            table.Rows.Add(row3);


            return table;
        }

        public void CheckEdit()
        {
            throw new NotImplementedException();
        }

        public void SetModifyContentDate()
        {
            throw new NotImplementedException();
        }

        public void CheckRelationsEdit()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public void SaveToDisk()
        {
            throw new NotImplementedException();
        }

        public DataTable GetEventsList(DBRecordSetParams paramSet, bool translateValues)
        {
            throw new NotImplementedException();
        }

        public void MakeBaseVersion()
        {
            throw new NotImplementedException();
        }

        public DateTime GetCheckOutDate()
        {
            throw new NotImplementedException();
        }

        public void SetRelationsAttributes(RelationAttributeValues[] relValues)
        {
            throw new NotImplementedException();
        }

        public DataTable GetObjectLinks(int attributeID)
        {
            throw new NotImplementedException();
        }

        public int ConnectToObject(long toObjectID)
        {
            throw new NotImplementedException();
        }

        public long ObjectID { get; }
        public long ID { get; }
        public int VersionID { get; }
        public DateTime CreateDate { get; }
        public int LCStep { get; set; }
        public long CheckoutBy { get; }
        public int ObjectType { get; set; }
        public long OwnerID { get; set; }
        public long CreatorID { get; }
        public string Caption { get; set; }
        public string NameInMessages { get; }
        public DateTime ModifyDate { get; }
        public int ObjectVerType { get; set; }
        public long ModificationID { get; }
        public string SiteID { get; }
        public bool IsBaseVersion { get; }
        public bool IsCreationMode { get; }
        public Guid GUID { get; set; }
        public Guid ObjectGUID { get; set; }
        public string SubjectAreas { get; }
        public long ParentVersionID { get; }
        public ObjectModifyModes ObjectModifyMode { get; }
        public ObjectFiltrationState FiltrationState { get; set; }
        public long ProjectID { get; set; }
        public int AccessLevel { get; set; }
        public int VersionsCount { get; }
    }
}