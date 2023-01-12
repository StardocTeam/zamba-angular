using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web.Http;
using System.Web.Http.Cors;
using Zamba.Core;
using ZambaWeb.RestApi.Controllers.Class;
using ZambaWeb.RestApi.Models.ResponseModel;

namespace ZambaWeb.RestApi.Controllers.Web
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Cache")]
    public class CacheController : ApiController
    {
       
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("CheckStructure")]
        public CacheResponseModel<int> CheckStructure(int userId)
        {
            CacheResponseModel<int> response = new CacheResponseModel<int>();
            CacheFactory ch = new CacheFactory();
            try
            {
                int result = ch.CheckDesignVersion();
                int resultCurrent = ch.CheckCurrentDesignVersion(userId);

                if (result > resultCurrent)
                {
                    CacheBusiness.ClearCachesByUser(userId);
                     ch.SetCurrentDesignVersion(result.ToString(), userId);
                    response.Success = true;
                    response.Data = result;
                }
                else
                {
                    response.Success = false;
                    response.Data = -1;
                }
                return response;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                response.Success = false;
                response.Data = -1;
                return response;
            }
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("ClearAllCache")]
        public bool ClearAllCache()
        {
            CacheFactory ch = new CacheFactory();
            try
            {

                    CacheBusiness.ClearCaches();
                    return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("ClearRulesCache")]
        public bool ClearRulesCache()
        {
            CacheFactory ch = new CacheFactory();
            try
            {

                CacheBusiness.ClearRulesCaches();

                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("ClearRightsCache")]
        public bool ClearRightsCache(Int64 userId)
        {
            CacheFactory ch = new CacheFactory();
            try
            {

                CacheBusiness.ClearRightsCaches(userId);
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("ClearStructureCache")]
        public bool ClearStructureCache()
        {
            CacheFactory ch = new CacheFactory();
            try
            {

                CacheBusiness.ClearStructureCaches();
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }


    }
}