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
        public CRUD_Service(IConfiguration configuration) : base(configuration)
        {
         
        }
        public void Init(ModelTypes DataType)
        {
            ItemIdentitySQL = $"{DataType}Id =";
            init(DataType);
        }
        public bool Create(T item)
        {
            return SQLCommand(SQLType.Create, "n", item.ToSQL());

        }
        public List<T> GetAll() 
        {
            SQLCommand(SQLType.GetAll);
            return Items;
        }

        public T GetFromId(int id)
        {
            //current.IdentitySQL
            SQLCommand(SQLType.GetSingle, $"{ItemIdentitySQL} {id}");
            return Item;

        }
        public bool Update(T item)
        {
            return SQLCommand(SQLType.Update, $"{ItemIdentitySQL} + {item.Identity()}", item.ToSQL());


        }

        public bool Delete(int id)
        {
            //current.IdentitySQL
            return SQLCommand(SQLType.Delete, $"{ItemIdentitySQL} + {id}");


        }

        public List<T> GetFiltered(string filter, ICrudService<T>.FilterType filterType)
        {
            /*switch (readmodel)
            {
                case ModelTypes.Conference:
                    break;
                case ModelTypes.Enrollment:
                    break;
                case ModelTypes.Event:
                    break;
                case ModelTypes.Floor:
                    break;
                case ModelTypes.Room:
                    break;
                case ModelTypes.Speaker:
                    break;
                case ModelTypes.User:
                    break;
                case ModelTypes.Venue:
                    break;
                default:
                    break;
            }*/
            return null;
        }
    }
}

