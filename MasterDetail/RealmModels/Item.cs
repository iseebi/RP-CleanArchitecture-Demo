using Realms;

namespace MasterDetail.RealmModels
{
    public class Item : RealmObject
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Models.Item ToItem()
        {
            return new Models.Item
            {
                Title = Title,
                Description = Description
            };
        }
    }
}