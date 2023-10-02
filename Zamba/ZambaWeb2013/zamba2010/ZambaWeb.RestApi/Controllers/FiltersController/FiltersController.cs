using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Zamba;
using Zamba.Core;
using Zamba.Filters;
using Zamba.Framework;
using ZambaWeb.RestApi.AuthorizationRequest;

namespace ZambaWeb.RestApi.Controllers.FiltersController
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RequestResponseController]
    public class FiltersController : ApiController
    {

        [Route("api/FiltersServices/GetFiltersByView")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult GetFiltersByView(genericRequest request)
        {

            try
            {
                long userID = request.UserId;
                long DocTypeID = long.Parse(request.Params["doctypeId"].ToString());
                string filterType = request.Params["filterType"].ToString();
                if (filterType.ToLower() == "myprocess" || filterType.ToLower() == "process" || filterType.ToLower() == "task")
                    filterType = "manual";
                else if (filterType == "Search")
                    filterType = "search";

                if (filterType == "search" || filterType == "manual")
                {
                    var filterComponents = new FiltersComponent();
                    List<IFilterElem> filterList = filterComponents.GetFiltersWebByView(DocTypeID, userID, filterType);
                    //filterList = CompleteFilterDataDescription(filterList);
                    filterList = CompleteFilterAutosustitutionCode(filterList);
                    string response = JsonConvert.SerializeObject(filterList);
                    return Ok(response);
                }
                else
                    throw new Exception("El valor de tipo de filtro tiene un valor invalido");

            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al obtener filtros: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al obtener filtros: " + ex.Message)));
            }
        }

        private List<IFilterElem> CompleteFilterDataDescription(List<IFilterElem> filters)
        {
            foreach (FilterElem filter in filters)
            {
                if (filter.IndexSubsType == IndexAdditionalType.AutoSustitución ||
                    filter.IndexSubsType == IndexAdditionalType.AutoSustituciónJerarquico ||
                     filter.IndexSubsType == IndexAdditionalType.DropDown ||
                      filter.IndexSubsType == IndexAdditionalType.DropDownJerarquico) {

                    filter.DataDescription = new AutoSubstitutionBusiness().getDescription(filter.Value.TrimStart('(').TrimEnd(')'), long.Parse(filter.Filter.ToLower().TrimStart('i')));
                }
            }
            return filters;
        }

        private List<IFilterElem> CompleteFilterAutosustitutionCode(List<IFilterElem> filters)
        {
            foreach (FilterElem filter in filters)
            {
                if (filter.IndexSubsType == IndexAdditionalType.AutoSustitución ||
                    filter.IndexSubsType == IndexAdditionalType.AutoSustituciónJerarquico ||
                     filter.IndexSubsType == IndexAdditionalType.DropDown ||
                      filter.IndexSubsType == IndexAdditionalType.DropDownJerarquico)
                {
                    filter.DataDescription = filter.Value;
                }
            }
            return filters;
        }


        [Route("api/FiltersServices/RemoveFilter")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult RemoveFilter(genericRequest request)
        {
            try
            {
                long userID = request.UserId;
                long indexId = long.Parse(request.Params["indexId"].ToString());
                String attribute = request.Params["attribute"].ToString();
                string comparator = request.Params["comparator"].ToString();
                string filterValue = request.Params["filterValue"].ToString();
                long docTypeId = long.Parse(request.Params["docTypeId"].ToString());

                if (indexId == 0)
                    attribute = GridColumns.GetColumnNameByAliasName(attribute);

                var filterComponents = new FiltersComponent();
                filterComponents.RemoveFilterWeb(docTypeId, userID, attribute, comparator, filterValue);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al eliminar un filtro: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al eliminar un filtro: " + ex.Message)));
            }
        }

        [Route("api/FiltersServices/RemoveFilterById")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult RemoveFilterById(genericRequest request)
        {
            try
            {
                long userID = request.UserId;
                long filterId = long.Parse(request.Params["filterId"].ToString());


                var filterComponents = new FiltersComponent();
                filterComponents.RemoveFilterWebById(filterId);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al eliminar un filtro: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al eliminar un filtro: " + ex.Message)));
            }
        }



        [Route("api/FiltersServices/RemoveZambaColumnsFilter")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult RemoveZambaColumnsFilter(genericRequest request)
        {
            try
            {
                long userID = request.UserId;
                long docTypeId = long.Parse(request.Params["docTypeId"].ToString());
                string filterType = request.Params["filterType"].ToString();
                if (filterType.ToLower() == "myprocess" || filterType.ToLower() == "process" || filterType.ToLower() == "task")
                    filterType = "manual";
                else if (filterType == "Search")
                    filterType = "search";
                var filterComponents = new FiltersComponent();
                filterComponents.RemoveZambaColumnsFilterWeb(docTypeId, userID, filterType);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al eliminar filtros por ZAMBA COLUMNS: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al eliminar filtros por ZAMBA COLUMNS: " + ex.Message)));
            }
        }

        [Route("api/FiltersServices/RemoveAllZambaColumnsFilter")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult RemoveAllZambaColumnsFilter(genericRequest request)
        {
            try
            {
                long userID = request.UserId;
                long docTypeId = long.Parse(request.Params["docTypeId"].ToString());
                string filterType = request.Params["filterType"].ToString();
                if (filterType.ToLower() == "myprocess" || filterType.ToLower() == "process" || filterType.ToLower() == "task")
                    filterType = "manual";
                else if (filterType == "Search")
                    filterType = "search";

                var filterComponents = new FiltersComponent();
                filterComponents.RemoveAllFilters(userID, filterType, docTypeId);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al eliminar filtros por ZAMBA COLUMNS: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al eliminar filtros por ZAMBA COLUMNS: " + ex.Message)));
            }
        }

        [Route("api/FiltersServices/SetEnabledFilter")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult SetEnabledFilter(genericRequest request)
        {
            try
            {
                long userID = request.UserId;
                long indexId = long.Parse(request.Params["indexId"].ToString());
                int enabled = (request.Params["enabled"]).ToString().ToLower() == "true" ? 1 : 0;
                String attribute = request.Params["attribute"].ToString();
                string comparator = request.Params["comparator"].ToString();
                string filterValue = request.Params["filterValue"].ToString();
                long docTypeId = long.Parse(request.Params["docTypeId"].ToString());

                if (indexId == 0)
                    attribute = GridColumns.GetColumnNameByAliasName(attribute);

                var filterComponents = new FiltersComponent();
                filterComponents.SetEnabledFilterWeb(docTypeId, userID, attribute, comparator, filterValue, enabled);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al cambiar el estado 'Enabled' de un filtro: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al cambiar el estado 'Enabled' de un filtro: " + ex.Message)));
            }
        }
        [Route("api/FiltersServices/SetEnabledFilterById")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult SetEnabledFilterById(genericRequest request)
        {
            try
            {
                long userID = request.UserId;
                long filterId = long.Parse(request.Params["filterId"].ToString());
                int enabled = (request.Params["enabled"]).ToString().ToLower() == "true" ? 1 : 0;

                var filterComponents = new FiltersComponent();
                filterComponents.SetEnabledFilterWebById(filterId, enabled);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al cambiar el estado 'Enabled' de un filtro: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al cambiar el estado 'Enabled' de un filtro: " + ex.Message)));
            }
        }

        [Route("api/FiltersServices/AddFilter")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult AddFilter(genericRequest request)
        {
            try
            {
                String dataDescription = String.Empty;
                IndexAdditionalType additionalType = IndexAdditionalType.NoIndex;
                long userID = request.UserId;
                var filterComponents = new FiltersComponent();
                //index = 0 para cuando es una columna fija de zamba
                long indexId = long.Parse(request.Params["indexId"].ToString());
                String attribute = request.Params["attribute"].ToString();//En caso de ser una columna fija, pasarle la key correspondiente en ZambaColumns
                IndexDataType dataType = (IndexDataType)Int32.Parse(request.Params["dataType"].ToString());//obtenerlo del indice
                string comparator = request.Params["comparator"].ToString();
                string filterValue = request.Params["filterValue"].ToString();
                long docTypeId = long.Parse(request.Params["docTypeId"].ToString());
                string description = request.Params["description"].ToString(); //obtenerlo del indice
                string filterType = request.Params["filterType"].ToString(); //obtenerlo del indice
                if (filterType.ToLower() == "myprocess" || filterType.ToLower() == "process" || filterType.ToLower() == "task")
                    filterType = "manual";
                else if (filterType == "Search")
                    filterType = "search";
                if (indexId != 0) {
                    additionalType = (IndexAdditionalType)IndexsBusiness.GetIndexDropDownType(indexId); //obtenerlo del indice
                    dataDescription = request.Params["dataDescription"].ToString();
                }
                IFilterElem filterElement = filterComponents.SetNewFilterWeb(indexId, attribute, dataType, userID, comparator, filterValue, docTypeId, true, description, additionalType, filterType);//'Task' porque asi se crean desde el cliente desktop
                return Ok(filterElement);
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al insertar un nuevo filtro: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al insertar un nuevo filtro: " + ex.Message)));
            }
        }

        [Route("api/FiltersServices/DeleteUserAssignedFilter")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult DeleteUserAssignedFilter(genericRequest request)
        {
            try
            {
                long userID = request.UserId;
                var filterComponents = new FiltersComponent();
                long docTypeId = long.Parse(request.Params["docTypeId"].ToString());
                string filterType = request.Params["filterType"].ToString(); //obtenerlo del indice
                if (filterType.ToLower() == "myprocess" || filterType.ToLower() == "process" || filterType.ToLower() == "task")
                    filterType = "manual";
                else if (filterType == "Search")
                    filterType = "search";
                filterComponents.DeleteUserAssignedFilter(userID, docTypeId, filterType);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al insertar un nuevo filtro: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al borrar filtro por usuario asignado: " + ex.Message)));
            }
        }

        [Route("api/FiltersServices/DeleteStepFilter")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult DeleteStepFilter(genericRequest request)
        {
            try
            {
                long userID = request.UserId;
                var filterComponents = new FiltersComponent();
                long docTypeId = long.Parse(request.Params["docTypeId"].ToString());
                string filterType = request.Params["filterType"].ToString(); //obtenerlo del indice
                if (filterType.ToLower() == "myprocess" || filterType.ToLower() == "process" || filterType.ToLower() == "task")
                    filterType = "manual";
                else if (filterType == "Search")
                    filterType = "search";
                filterComponents.DeleteStepFilter(userID, docTypeId, filterType);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al insertar un nuevo filtro: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al borrar filtro por usuario asignado: " + ex.Message)));
            }
        }
        

        [Route("api/FiltersServices/UpdateFilterValue")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult UpdateFilterValue(genericRequest request)
        {
            try
            {
                long userID = request.UserId;
                var filterComponents = new FiltersComponent();
                long zfilterId = long.Parse(request.Params["zfilterId"].ToString());
                string filterValue = request.Params["valToUpdate"].ToString();
                filterComponents.UpdateFilterValue(zfilterId, filterValue);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al Actualizar un filtro: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al Actualizar un filtro: " + ex.Message)));
            }
        }


        [Route("api/FiltersServices/SetDisabledAllFiltersByUser")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult SetDisabledAllFiltersByUser(genericRequest request)
        {
            try
            {
                long userID = request.UserId;
                string filterType = request.Params["filterType"].ToString();
                if (filterType.ToLower() == "myprocess" || filterType.ToLower() == "process" || filterType.ToLower() == "task")
                    filterType = "manual";
                else if (filterType == "Search")
                    filterType = "search";
                else

                if(filterType != "manual" && filterType != "search")
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("el filter type no es correcto: ")));

                var filterComponents = new FiltersComponent();
                filterComponents.SetDisabledAllFiltersByUser(userID, filterType);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al Deshabilitar los filtros al buscar: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al Deshabilitar los filtros al buscar: " + ex.Message)));
            }
        }

        [Route("api/FiltersServices/SetDisabledAllFiltersByUserViewDoctype")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult SetDisabledAllFiltersByUserViewDoctype(genericRequest request)
        {
            try
            {
                long userID = request.UserId;
                string filterType = request.Params["filterType"].ToString();
                long docTypeId = long.Parse(request.Params["docTypeId"].ToString());
                if (filterType.ToLower() == "myprocess" || filterType.ToLower() == "process" || filterType.ToLower() == "task")
                    filterType = "manual";
                else if (filterType == "Search")
                    filterType = "search";
                var filterComponents = new FiltersComponent();
                filterComponents.SetDisabledAllFiltersByUserViewDoctype(userID, filterType, docTypeId);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al Deshabilitar los filtros al buscar: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al Deshabilitar los filtros al buscar: " + ex.Message)));
            }
        }


        [Route("api/FiltersServices/SetEnabledAllFiltersByUserViewDoctype")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [isGenericRequest]
        public IHttpActionResult SetEnabledAllFiltersByUserViewDoctype(genericRequest request)
        {
            try
            {
                long userID = request.UserId;
                string filterType = request.Params["filterType"].ToString();
                long docTypeId = long.Parse(request.Params["docTypeId"].ToString());
                if (filterType.ToLower() == "myprocess" || filterType.ToLower() == "process" || filterType.ToLower() == "task")
                    filterType = "manual";
                else if (filterType == "Search")
                    filterType = "search";
                var filterComponents = new FiltersComponent();
                filterComponents.SetEnabledAllFiltersByUserViewDoctype(userID, filterType, docTypeId);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al habilitar los filtros al buscar: " + ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Error al habilitar los filtros al buscar: " + ex.Message)));
            }
        }

    }
}