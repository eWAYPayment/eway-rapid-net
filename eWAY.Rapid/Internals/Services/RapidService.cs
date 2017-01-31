using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Reflection;
using eWAY.Rapid.Internals.Enums;
using eWAY.Rapid.Internals.Request;
using eWAY.Rapid.Internals.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace eWAY.Rapid.Internals.Services
{
    /// <summary>
    /// Internal Client class that does the invocation of Rapid native API call
    /// </summary>
    internal class RapidService : IRapidService
    {
        private string _rapidEndpoint;
        private string _authenticationHeader;
        private int? _version;
        private bool _isValidEndPoint;
        private bool _isValidCredentials;

        private const string ACCESS_CODES = "AccessCodes";
        private const string ACCESS_CODE_RESULT = "AccessCode/{0}";
        private const string CANCEL_AUTHORISATION = "CancelAuthorisation";
        private const string DIRECT_PAYMENT = "Transaction";
        private const string ACCESS_CODES_SHARED = "AccessCodesShared";
        private const string CAPTURE_PAYMENT = "CapturePayment";
        private const string REFUND_PAYMENT = "Transaction/{0}/Refund";
        private const string QUERY_TRANSACTION = "Transaction/{0}";
        private const string QUERY_CUSTOMER = "Customer/{0}";
        private const string TRANSACTION_FILTER_INVOICE_NUMBER = "Transaction/InvoiceNumber/{0}";
        private const string TRANSACTION_FILTER_INVOICE_REF = "Transaction/InvoiceRef/{0}";
        private const string SETTLEMENT_SEARCH = "Search/Settlement?{0}";


        public IMappingService MappingService { get; set; }
        
        public RapidService(string apiKey, string password, string endpoint, SecurityProtocolType? securityProtocol)
        {
            if(securityProtocol.HasValue)
                ServicePointManager.SecurityProtocol = securityProtocol.Value;

            SetCredentials(apiKey, password);
            SetRapidEndpoint(endpoint);
        }


        public DirectCancelAuthorisationResponse CancelAuthorisation(DirectCancelAuthorisationRequest request)
        {
            return JsonPost<DirectCancelAuthorisationRequest, DirectCancelAuthorisationResponse>(request, CANCEL_AUTHORISATION);
        }

        public DirectCapturePaymentResponse CapturePayment(DirectCapturePaymentRequest request)
        {
            return JsonPost<DirectCapturePaymentRequest, DirectCapturePaymentResponse>(request, CAPTURE_PAYMENT);
        }

        public CreateAccessCodeResponse CreateAccessCode(CreateAccessCodeRequest request)
        {
            return JsonPost<CreateAccessCodeRequest, CreateAccessCodeResponse>(request, ACCESS_CODES);
        }

        public CreateAccessCodeResponse UpdateCustomerCreateAccessCode(CreateAccessCodeRequest request)
        {
            request.Method = Method.UpdateTokenCustomer;
            return JsonPut<CreateAccessCodeRequest, CreateAccessCodeResponse>(request, ACCESS_CODES);
        }

        public CreateAccessCodeSharedResponse CreateAccessCodeShared(CreateAccessCodeSharedRequest request)
        {
            return JsonPost<CreateAccessCodeSharedRequest, CreateAccessCodeSharedResponse>(request, ACCESS_CODES_SHARED);
        }

        public CreateAccessCodeSharedResponse UpdateCustomerCreateAccessCodeShared(CreateAccessCodeSharedRequest request)
        {
            request.Method = Method.UpdateTokenCustomer;
            return JsonPut<CreateAccessCodeSharedRequest, CreateAccessCodeSharedResponse>(request, ACCESS_CODES_SHARED);
        }

        public GetAccessCodeResultResponse GetAccessCodeResult(GetAccessCodeResultRequest request)
        {
            return JsonGet<GetAccessCodeResultResponse>(string.Format(ACCESS_CODE_RESULT, request.AccessCode));
        }

        public DirectPaymentResponse DirectPayment(DirectPaymentRequest request)
        {
            return JsonPost<DirectPaymentRequest, DirectPaymentResponse>(request, DIRECT_PAYMENT);
        }

        public DirectPaymentResponse UpdateCustomerDirectPayment(DirectPaymentRequest request)
        {
            request.Method = Method.UpdateTokenCustomer;
            return JsonPut<DirectPaymentRequest, DirectPaymentResponse>(request, DIRECT_PAYMENT);
        }

        public DirectAuthorisationResponse DirectAuthorisation(DirectAuthorisationRequest request)
        {
            return JsonPost<DirectAuthorisationRequest, DirectAuthorisationResponse>(request, DIRECT_PAYMENT);
        }

        public DirectCustomerResponse DirectCustomerCreate(DirectCustomerRequest request)
        {
            return JsonPost<DirectCustomerRequest, DirectCustomerResponse>(request, DIRECT_PAYMENT);
        }

        public DirectCustomerSearchResponse DirectCustomerSearch(DirectCustomerSearchRequest request)
        {
            return JsonGet<DirectCustomerSearchResponse>(string.Format(QUERY_CUSTOMER, request.TokenCustomerID));
        }

        public DirectRefundResponse DirectRefund(DirectRefundRequest request)
        {
            return JsonPost<DirectRefundRequest, DirectRefundResponse>(request, string.Format(REFUND_PAYMENT, request.Refund.TransactionID));
        }

        public TransactionSearchResponse QueryTransaction(long transactionID)
        {
            var method = string.Format(QUERY_TRANSACTION, transactionID);
            return JsonGet<TransactionSearchResponse>(method);
        }

        public TransactionSearchResponse QueryTransaction(string accessCode)
        {
            var method = string.Format(QUERY_TRANSACTION, accessCode);
            return JsonGet<TransactionSearchResponse>(method);
        }

        public TransactionSearchResponse QueryInvoiceRef(string invoiceRef)
        {
            var method = string.Format(TRANSACTION_FILTER_INVOICE_REF, invoiceRef);
            return JsonGet<TransactionSearchResponse>(method);
        }

        public TransactionSearchResponse QueryInvoiceNumber(string invoiceNumber)
        {
            var method = string.Format(TRANSACTION_FILTER_INVOICE_NUMBER, invoiceNumber);
            return JsonGet<TransactionSearchResponse>(method);
        }

        public DirectSettlementSearchResponse SettlementSearch(string request)
        {
            var method = string.Format(SETTLEMENT_SEARCH, request);
            return JsonGet<DirectSettlementSearchResponse>(method);
        }

        public TResponse JsonPost<TRequest, TResponse>(TRequest request, string method)
            where TRequest : class
            where TResponse : BaseResponse, new()
        {
            var jsonString = JsonConvert.SerializeObject(request,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Converters = new JsonConverter[] {new StringEnumConverter()}
                });

            var endpointUrl = _rapidEndpoint + method;
            // create a webrequest
            var webRequest = (HttpWebRequest)WebRequest.Create(endpointUrl);
            var response = new TResponse();
            try
            {
                AddHeaders(webRequest, HttpMethods.POST.ToString());
                webRequest.ContentLength = Encoding.UTF8.GetByteCount(jsonString);
                var result = GetWebResponse(webRequest, jsonString);
                response = JsonConvert.DeserializeObject<TResponse>(result);
            }
            catch (WebException ex)
            {
                var errors = HandleWebException(ex);
                response.Errors = errors;
            }
            return response;
        }

        public TResponse JsonPut<TRequest, TResponse>(TRequest request, string method)
            where TRequest : class
            where TResponse : BaseResponse, new()
        {
            var jsonString = JsonConvert.SerializeObject(request,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Converters = new JsonConverter[] { new StringEnumConverter() }
                });

            var endpointUrl = _rapidEndpoint + method;
            // create a webrequest
            var webRequest = (HttpWebRequest)WebRequest.Create(endpointUrl);
            var response = new TResponse();
            try
            {
                //TODO:
                //This should be a PUT
                AddHeaders(webRequest, HttpMethods.POST.ToString());
                webRequest.ContentLength = Encoding.UTF8.GetByteCount(jsonString);
                var result = GetWebResponse(webRequest, jsonString);
                response = JsonConvert.DeserializeObject<TResponse>(result);
            }
            catch (WebException ex)
            {
                var errors = HandleWebException(ex);
                response.Errors = errors;
            }
            return response;
        }

        public TResponse JsonGet<TResponse>(string method)
            where TResponse : BaseResponse, new()
        {
            var endpointUrl = _rapidEndpoint + method;
            // create a webrequest
            var webRequest = (HttpWebRequest)WebRequest.Create(endpointUrl);
            var response = new TResponse();
            try
            {
                AddHeaders(webRequest, HttpMethods.GET.ToString());
                string result = GetWebResponse(webRequest);
                if (String.IsNullOrEmpty(result))
                {
                    var errors = RapidSystemErrorCode.COMMUNICATION_ERROR;
                    response.Errors = errors;
                }
                else
                {
                    response = JsonConvert.DeserializeObject<TResponse>(result);
                }
            }
            catch (WebException ex)
            {
                var errors = HandleWebException(ex);
                response.Errors = errors;
            }

            return response;
        }

        private void AddHeaders(HttpWebRequest webRequest, string httpMethod)
        {
            // add authentication to request
            webRequest.Headers.Add("Authorization", _authenticationHeader);
            webRequest.UserAgent = "eWAY SDK .NET " + Assembly.GetExecutingAssembly().GetName().Version;
            webRequest.Method = httpMethod;
            webRequest.ContentType = "application/json";

            if (_version.HasValue)
            {
                webRequest.Headers.Add("X-EWAY-APIVERSION", _version.ToString());
            }
        }

        private string HandleWebException(WebException ex)
        {
            if (ex.Response == null)
            {
                return RapidSystemErrorCode.COMMUNICATION_ERROR;
            }

            var errorCode = ((HttpWebResponse)ex.Response).StatusCode;

            if (errorCode == HttpStatusCode.Unauthorized ||
                errorCode == HttpStatusCode.Forbidden ||
                errorCode == HttpStatusCode.NotFound)
            {
                _isValidCredentials = false;
                return RapidSystemErrorCode.AUTHENTICATION_ERROR;
            }
            return RapidSystemErrorCode.COMMUNICATION_ERROR;
        }

        public virtual string GetWebResponse(WebRequest webRequest, string content = null)
        {
            if (content != null)
            {
                using (new MemoryStream())
                {
                    using (var writer = new StreamWriter(webRequest.GetRequestStream()))
                    {
                        writer.Write(content);
                        writer.Close();
                    }
                }
            }

            string result;
            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            using (var stream = webResponse.GetResponseStream())
            {
                if (stream == null) return null;
                var sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }

        public string GetRapidEndpoint()
        {
            return _rapidEndpoint;
        }

        public void SetRapidEndpoint(string value)
        {
            switch (value)
            {
                case "Production":
                    _rapidEndpoint = GlobalEndpoints.PRODUCTION;
                    break;
                case "Sandbox":
                    _rapidEndpoint = GlobalEndpoints.SANDBOX;
                    break;
                default:
                    if (!value.EndsWith("/"))
                    {
                        value += "/";
                    }
                    _rapidEndpoint = value;
                    break;
            }
            _isValidEndPoint = Uri.IsWellFormedUriString(_rapidEndpoint, UriKind.Absolute);
        }

        public void SetCredentials(string apiKey, string password)
        {
            _authenticationHeader = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(apiKey + ":" + password));
            _isValidCredentials = !string.IsNullOrWhiteSpace(apiKey) && !string.IsNullOrWhiteSpace(password);
        }

        public void SetVersion(int version)
        {
            _version = version;
        }

        public bool IsValid()
        {
            return _isValidCredentials && _isValidEndPoint;
        }

        public List<string> GetErrorCodes()
        {
            var errors = new List<string>();

            if (!_isValidEndPoint)
            {
                errors.Add(RapidSystemErrorCode.INVALID_ENDPOINT_ERROR);
            }

            if (!_isValidCredentials)
            {
                errors.Add(RapidSystemErrorCode.INVALID_CREDENTIAL_ERROR);
            }

            return errors;
        }
    }
}
