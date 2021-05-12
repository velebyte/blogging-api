using BloggingAPI.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAPI.Domain.Mappers
{
    public static class TagsMapper
    {
        public static List<string> ToStringList(this List<Tag> tags)
        {
            var tagsList = new List<string>();

            if (tags.Count > 0)
            {
                foreach (var tag in tags)
                {
                    tagsList.Add(tag.Name);
                }
            }

            return tagsList;
        }
    }
}
