namespace Transat;

public class NonResilientPublisher(FaillibleQos0Storage storage1, FaillibleQos0Storage storage2)
{
    private string _id = Guid.NewGuid().ToString(format:"n");
    public void Send(int message)
    {
        var uniqueId = $"{_id}-{Guid.NewGuid().ToString(format:"n")}";

        while (!storage1.Data.Keys.Any(key => key.StartsWith(uniqueId)))
        {
            storage1.Store(uniqueId, message);
        }
        while (!storage2.Data.Keys.Any(key => key.StartsWith(uniqueId)))
        {
            storage2.Store(uniqueId, message);
        }
        //do
        //{
        //    if (!storage1.Data.Keys.Any(key => key.Contains(_id)))
        //    {
        //        storage1.Store(_id, message);
        //    }
        //    if (!storage2.Data.Keys.Any(key => key.Contains(_id)))
        //    {
        //        storage2.Store(_id, message);
        //    }
        //} while (!storage1.Data.Keys.Any(key => key.Contains(_id)) && !storage2.Data.Keys.Any(key => key.Contains(_id)));

        //while (!success1)
        //{
        //    storage1.Store(_id, message);
        //    foreach (var item in storage1.Data)
        //    {
        //        if (item.Key.Contains(_id))
        //        {
        //            success1 = true;
        //        }
        //    }
        //}

        //while (!success2)
        //{
        //    storage2.Store(_id, message);
        //    foreach (var item in storage2.Data)
        //    {
        //        if (item.Key.Contains(_id))
        //        {
        //            success2 = true;
        //        } else
        //        {
        //            storage2.Store(_id, message);
        //        }
        //    }
        //}
        //foreach (var item in storage1.Data)
        //{
        //    foreach (var item2 in storage2.Data)
        //    {
        //        if (item.Key == item2.Key)
        //        {
        //            success = true;
        //        }
        //    }
        //}
        //if (!success) 
        //{
        //    storage2.Store(_id, message);
        //}
    }
}