using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Core.Utils;

namespace SSCMS.Core.Repositories.ContentTagRepository
{
    public partial class ContentTagRepository
    {
        private string GetCacheKey(int siteId)
        {
            return CacheUtils.GetListKey(TableName, siteId);
        }

        public async Task<List<string>> GetTagNamesAsync(int siteId)
        {
            return await _repository.GetAllAsync<string>(Q
                .Select(nameof(ContentTag.TagName))
                .Where(nameof(ContentTag.SiteId), siteId)
                .WhereNotNull(nameof(ContentTag.TagName))
                .WhereNot(nameof(ContentTag.TagName), string.Empty)
                .Distinct()
                .OrderBy(nameof(ContentTag.TagName))
                .CachingGet(GetCacheKey(siteId))
            );
        }
    }
}