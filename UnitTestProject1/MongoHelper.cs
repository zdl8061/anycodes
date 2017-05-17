/*----------------------------------------------------------------
 *  Copyright (C) 2015 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：MongoHelper
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2017/5/17 星期三 下午 15:31:42
 *  
 *  功能描述：
 *          1、
 *          2、 
 * 
 *  修改标识：  
 *  修改描述：
 *  待 完 善：
 *          1、 
----------------------------------------------------------------*/
namespace UnitTestProject1
{
    using System;  
    using MongoDB.Bson;  
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using System.Collections;  
    using System.Linq;  
    using System.Collections.Generic;  
  
    public sealed class MongoHelper<TClass> : IDisposable
{
    private static volatile MongoHelper<TClass> instance = null;
    public Boolean IsDisposed { get; private set; }
    private static object threadSafeLocker = new object();
    private MongoServer DBServer = null;

    private MongoHelper(MongoServerSettings server_settings)
    {
        DBServer = new MongoServer(server_settings);
        if (DBServer.State != MongoServerState.Connected)
            DBServer.Connect();
    }

    public static MongoHelper<TClass> GetDBInstance(string serviceInfo)
    {
        if (null == instance)
        {
            lock (threadSafeLocker)
            {
                if (null == instance || instance.IsDisposed || instance.DBServer.State != MongoServerState.Connected)
                {
                    List<MongoServerAddress> slist = new List<MongoServerAddress>();
                    MongoServerAddress def = new MongoServerAddress(serviceInfo, 27017);
                    slist.Add(def);
                    MongoServerSettings settings = new MongoServerSettings();
                    settings.ConnectionMode = ConnectionMode.Direct;
                    settings.ConnectTimeout = TimeSpan.FromSeconds(30);
                    settings.GuidRepresentation = GuidRepresentation.CSharpLegacy;
                    settings.IPv6 = false;
                    settings.MaxConnectionIdleTime = TimeSpan.FromMinutes(10);
                    settings.MaxConnectionLifeTime = TimeSpan.FromMinutes(30);
                    settings.MaxConnectionPoolSize = 100;
                    settings.MinConnectionPoolSize = 0;
                    //settings.SafeMode = new SafeMode(true);
                    settings.Servers = slist.Cast<MongoServerAddress>();
                    //settings.SlaveOk = true;
                    settings.SocketTimeout = TimeSpan.FromSeconds(30);
                    settings.WaitQueueSize = 250;
                    settings.WaitQueueTimeout = TimeSpan.FromMilliseconds(500);
                    instance = new MongoHelper<TClass>(settings);
                }
            }
        }

        return instance;
    }

    public long Count(String database_name
        , String collection_name
        , IMongoQuery query)
    {
        return instance.DBServer.GetDatabase(database_name)
            .GetCollection<TClass>(collection_name)
            .Count(query);
    }

    public TClass FindOneById(String database_name
        , String collection_name
        , BsonValue id)
    {
        return instance.DBServer.GetDatabase(database_name)
            .GetCollection<TClass>(collection_name)
            .FindOneByIdAs<TClass>(id);
    }

    public TClass FindOne(String database_name
        , String collection_name
        , IMongoQuery query)
    {
        return instance.DBServer.GetDatabase(database_name)
            .GetCollection<TClass>(collection_name)
            .FindOneAs<TClass>(query);
    }


    public MongoCursor<TClass> FindAll(String database_name
        , String collection_name
        , IMongoQuery query)
    {
        return instance.DBServer.GetDatabase(database_name)
            .GetCollection<TClass>(collection_name)
            .FindAs<TClass>(query);
    }

    public MongoCursor<TClass> FindPage(String database_name
        , String collection_name
        , IMongoQuery query
        , IMongoSortBy sortby
        , int pageIndex
        , int pageSize)
    {
        MongoCursor<TClass> cursor = instance.DBServer.GetDatabase(database_name)
            .GetCollection<TClass>(collection_name)
            .Find(query)
            .SetSortOrder(sortby);
        cursor.Skip = pageIndex * pageSize;
        cursor.Limit = pageSize;
        return cursor;
    }



    public bool IsExists(String database_name
        , String collection_name
        , IMongoQuery query)
    {
        return instance.DBServer.GetDatabase(database_name)
            .GetCollection<TClass>(collection_name)
            .Count(query) > 0;
    }

    //public SafeModeResult Remove(String database_name
    //    , String collection_name
    //    , IMongoQuery query)
    //{
    //    return Remove(database_name
    //        , collection_name
    //        , Query.And(query)
    //        );
    //}

    //public SafeModeResult RemoveById(String database_name
    //    , String collection_name
    //    , BsonValue id)
    //{
    //    return Remove(database_name
    //        , collection_name
    //        , Query.EQ("_id", id));
    //}

    //public SafeModeResult RemoveAll(String database_name
    //    , String collection_name)
    //{
    //    SafeModeResult result = instance.DBServer.GetDatabase(database_name)
    //        .GetCollection<TClass>(collection_name)
    //        .RemoveAll();
    //    return result;
    //}

    //public SafeModeResult Add(String database_name
    //    , String collection_name
    //    , TClass document)
    //{
    //    SafeModeResult result = instance.DBServer.GetDatabase(database_name)
    //        .GetCollection<TClass>(collection_name)
    //        .Insert(document);
    //    return result;
    //}

    //public IEnumerable<SafeModeResult> AddBatch(String database_name
    //    , String collection_name
    //    , IEnumerable<TClass> documents)
    //{
    //    IEnumerable<SafeModeResult> results = instance.DBServer.GetDatabase(database_name)
    //        .GetCollection<TClass>(collection_name)
    //        .InsertBatch(documents);
    //    return results;
    //}

    //public SafeModeResult Update(string database_name
    //    , string collection_name
    //    , BsonValue key, TClass document)
    //{
    //    return Update(database_name
    //        , collection_name
    //        , Query.And(Query.EQ("_id", BsonValue.Create(key)))
    //        , document);
    //}

    //public SafeModeResult Update(string database_name
    //    , string collection_name
    //    , IMongoQuery query, TClass document)
    //{
    //    return instance.DBServer.GetDatabase(database_name)
    //        .GetCollection<TClass>(collection_name)
    //        .Update(query, new UpdateDocument(document.ToBsonDocument<TClass>()));
    //}

    #region IDisposable Members  
    public void Dispose()
    {
        try
        {
            instance.DBServer.Disconnect();
        }
        catch { }

        IsDisposed = true;
    }
    #endregion

    public IEnumerable<string> GetDatabaseNames()
    {
        return instance.DBServer.GetDatabaseNames();
    }

    public MongoDatabase GetDatabase(string dbname)
    {
        return instance.DBServer.GetDatabase(dbname);
    }

    public IEnumerable<BsonValue> Distinct(string dbname, String collectionname, string key)
    {
        return Distinct(dbname, collectionname, key, Query.Null);
    }

    public IEnumerable<BsonValue> Distinct(string dbname, String collectionname, string key, IMongoQuery query)
    {
        return instance.DBServer.GetDatabase(dbname).GetCollection(collectionname).Distinct(key, query);
    }
}  
}  
