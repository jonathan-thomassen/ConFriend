using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFriend.Services
{
    public class CRUD_Service<T> : SQLService2<T>, ICrudService<T> where T : IModel
    {
        //    <T> 
        string ItemIdentitySQL;
        string ItemIdentitySQLExtra;
        bool IsComposit;

        public int GetLastId {
            get { return LastId; }
        }

      
        public CRUD_Service(IConfiguration configuration) : base(configuration)
        {
            IsComposit = false;
        }
        public void Init(ModelTypes DataType)
        {
            ItemIdentitySQL = $"{DataType}Id =";
            init(DataType);
        }
        public void Init(ModelTypes DataType,string name)
        {
            ItemIdentitySQL = $"{name}Id =";
            init(DataType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DataTypeA"></param>
        /// The first part of the composite key
        /// <param name="DataTypeB"></param>
        /// THe second part of the composite key
        /// <param name="TrueDataType"></param>
        /// The actual DataType in use
        public void Init_Composite(ModelTypes DataTypeA, ModelTypes DataTypeB, ModelTypes TrueDataType)
        {
            IsComposit = true;
            ItemIdentitySQL = $"{DataTypeA}Id =";
            ItemIdentitySQLExtra = $"{DataTypeB}Id =";
            init(TrueDataType);
        }
        public async Task<bool> Create(T item)
        {
            return await SQLCommand(SQLType.Create, "n", item.ToSQL());
        }

        public async Task<List<T>> GetAll() 
        {
            await SQLCommand(SQLType.GetAll);
            return Items;
        }

        public async Task<T> GetFromId(int id, int id2 = 0)
        {
            //current.IdentitySQL
            if (IsComposit)
                await SQLCommand(SQLType.GetSingle, $"{ItemIdentitySQL} {id} AND {ItemIdentitySQLExtra} {id2}"); 
            else
                await SQLCommand(SQLType.GetSingle, $"{ItemIdentitySQL} {id}");

            return Item;
        }
        public async Task<T> GetFromField(string customField)
        {
            await SQLCommand(SQLType.GetSingle, $"{customField}");
            return Item;
        }
        public async Task<bool> Update(T item)
        {
            return await SQLCommand(SQLType.Update, ItemIdentitySQL, item.ToSQL());
        }

        public async Task<bool> Delete(int id,int id2 = 0)
        {
            if(IsComposit)
                return await SQLCommand(SQLType.Delete, $"{ItemIdentitySQL} {id} AND {ItemIdentitySQLExtra} {id2}"); 
            else
                return await SQLCommand(SQLType.Delete, $"{ItemIdentitySQL} {id}");
        }
        //current.IdentitySQL

   
        public async Task<List<T>> GetFiltered(int filterId, ModelTypes joinId , ModelTypes myId = ModelTypes.none)
        {
            if(myId == ModelTypes.none)
                await SQLCommand(SQLType.JoinOn, $"{joinId}.{joinId}",$"{filterId}");
            else
                await SQLCommand(SQLType.JoinOn, $"{myId}.{joinId}", $"{filterId}");
            return Items;
        }
        public async Task<List<T>> GetFiltered(ModelTypes joinId, ModelTypes myId = ModelTypes.none)
        {
            if (myId == ModelTypes.none)
                await SQLCommand(SQLType.JoinOn, $"{joinId}.{joinId}");
            else
                await SQLCommand(SQLType.JoinOn, $"{myId}.{joinId}");
            return Items;
        }
        public void ClearItemData()
        {
            if(Items != null)Items.Clear();
            Item = default;
        }
       
    }
}

