﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Stack247.WebScrapper.Constants;
using Stack247.WebScrapper.Scrapper;
using CsQuery;

namespace Stack247.WebScrapper.Helpers
{
    public class Http
    {
        public static string GetStringFromUrl(string url)
        {
            return GetStringFromUrl(url, null, null, null);
        }

        public static string GetStringFromUrl(string url, Cookie cookie)
        {
            return GetStringFromUrl(url, cookie, null, null);
        }

        public static string GetStringFromUrl(string url, CookieCollection cookies)
        {
            return GetStringFromUrl(url, null, cookies, null);
        }

        public static string GetStringFromUrl(string url, Dictionary<HttpRequestHeader, string> headers)
        {
            return GetStringFromUrl(url, null, null, headers);
        }

        public static string GetStringFromUrl(string url, Cookie cookie, Dictionary<HttpRequestHeader, string> headers)
        {
            return GetStringFromUrl(url, cookie, null, headers);
        }

        public static string GetStringFromUrl(string url, CookieCollection cookies, Dictionary<HttpRequestHeader, string> headers)
        {
            return GetStringFromUrl(url, null, cookies, headers);
        }

        public static string GetStringFromUrl(string url, Cookie cookie, CookieCollection cookies, Dictionary<HttpRequestHeader, string> headers)
        {
            var _response = GetResponseFromUrl(url, cookie, cookies, headers);

            return GetStringFromWebResponse(_response);
        }

        public static string GetStringFromWebResponse(HttpWebResponse response)
        {
            if (response != null)
                using (Stream _inputStream = response.GetResponseStream())
                {
                    var _inputStreamReader = new StreamReader(_inputStream);
                    return _inputStreamReader.ReadToEnd();
                }

            return null;
        }

        public static HttpWebResponse GetResponseFromUrl(string url)
        {
            return GetResponseFromUrl(url, null, null, null);
        }

        public static HttpWebResponse GetResponseFromUrl(string url, Cookie cookie)
        {
            return GetResponseFromUrl(url, cookie, null, null);
        }

        public static HttpWebResponse GetResponseFromUrl(string url, CookieCollection cookies)
        {
            return GetResponseFromUrl(url, null, cookies, null);
        }

        public static HttpWebResponse GetResponseFromUrl(string url, Dictionary<HttpRequestHeader, string> headers)
        {
            return GetResponseFromUrl(url, null, null, headers);
        }

        public static HttpWebResponse GetResponseFromUrl(string url, Cookie cookie, Dictionary<HttpRequestHeader, string> headers)
        {
            return GetResponseFromUrl(url, cookie, null, headers);
        }

        public static HttpWebResponse GetResponseFromUrl(string url, CookieCollection cookies, Dictionary<HttpRequestHeader, string> headers)
        {
            return GetResponseFromUrl(url, null, cookies, headers);
        }

        public static HttpWebResponse GetResponseFromUrl(string url, Cookie cookie, CookieCollection cookies, Dictionary<HttpRequestHeader, string> headers)
        {
            return RequestResponseFromUrl(url, null, cookie, cookies, headers, null);
        }

        public static HttpWebResponse PostResponseFromUrl(string url, Dictionary<HttpRequestHeader, string> headers)
        {
            return PostResponseFromUrl(url, null, null, headers, null);
        }

        public static HttpWebResponse PostResponseFromUrl(string url, Dictionary<HttpRequestHeader, string> headers, string body)
        {
            return PostResponseFromUrl(url, null, null, headers, body);
        }

        public static HttpWebResponse PostResponseFromUrl(string url, Cookie cookie, Dictionary<HttpRequestHeader, string> headers, string body)
        {
            return PostResponseFromUrl(url, cookie, null, headers, body);
        }

        public static HttpWebResponse PostResponseFromUrl(string url, CookieCollection cookies, Dictionary<HttpRequestHeader, string> headers, string body)
        {
            return PostResponseFromUrl(url, null, cookies, headers, body);
        }

        public static HttpWebResponse PostResponseFromUrl(string url, Cookie cookie, CookieCollection cookies, Dictionary<HttpRequestHeader, string> headers, string body)
        {
            return RequestResponseFromUrl(url, WebRequestMethods.Http.Post, cookie, cookies, headers, body);
        }

        public static HttpWebResponse RequestResponseFromUrl(string url, string method, Cookie cookie, CookieCollection cookies, Dictionary<HttpRequestHeader, string> headers, string body)
        {
            var _request = (HttpWebRequest)WebRequest.Create(url);

            if (method != null)
                _request.Method = method;

            if (cookie != null)
            {
                if (_request.CookieContainer == null)
                    _request.CookieContainer = new CookieContainer();

                _request.CookieContainer.Add(cookie);
            }

            if (cookies != null && cookies.Count > 0)
            {
                if (_request.CookieContainer == null)
                    _request.CookieContainer = new CookieContainer();

                _request.CookieContainer.Add(cookies);
            }

            if (headers != null)
            {
                foreach (var _header in headers)
                {
                    if (_header.Key == HttpRequestHeader.UserAgent)
                        _request.UserAgent = _header.Value;
                    else if (_header.Key == HttpRequestHeader.Accept)
                        _request.Accept = _header.Value;
                    else if (_header.Key == HttpRequestHeader.ContentType)
                        _request.ContentType = _header.Value;
                    else if (_header.Key == HttpRequestHeader.Referer)
                        _request.Referer = _header.Value;
                    else if (_header.Key == HttpRequestHeader.ContentLength)
                        _request.Referer = _header.Value;
                    else if (_header.Key == HttpRequestHeader.Host)
                        _request.Referer = _header.Value;
                    else
                        _request.Headers.Add(_header.Key, _header.Value);
                }
            }

            if (body != null)
            {
                var _bodyData = Encoding.UTF8.GetBytes(body);
                using (var _stream = _request.GetRequestStream())
                {
                    _stream.Write(_bodyData, 0, _bodyData.Length);
                    _stream.Close();
                }
            }

            var _response = (HttpWebResponse)_request.GetResponse();

            if ((_response.StatusCode == HttpStatusCode.OK ||
                _response.StatusCode == HttpStatusCode.Moved ||
                _response.StatusCode == HttpStatusCode.Redirect))
            {
                return _response;
            }

            return null;
        }

        public static string GetValueFromDom(string dom, string selector, GetValueMethods getValueMethod)
        {
            // TODO: 7.29.2014 - Write reusable code for this method and GetValuesFromDom

            // CSQuery
            // https://github.com/jamietre/CsQuery

            CQ _dom = dom;
            CQ _element;
            if (!string.IsNullOrEmpty(selector))
                _element = _dom[selector];
            else
                _element = _dom;

            var _result = string.Empty;

            // TODO: 7.29.2014 - Move to Strategy pattern
            if (getValueMethod == GetValueMethods.Html)
                _result = _element.Html();
            else if (getValueMethod == GetValueMethods.Text)
                _result = _element.Text();
            else if (getValueMethod == GetValueMethods.AttributeHref)
                _result = _element.Attr("href");
            else if (getValueMethod == GetValueMethods.AttributeSrc)
                _result = _element.Attr("src");

            return _result.Trim();
        }

        public static ICollection<string> GetValuesFromDom(string dom, string selector, GetValueMethods getValueMethod)
        {
            // TODO: 7.29.2014 - Write reusable code for this method and GetValueFromDom

            // CSQuery
            // https://github.com/jamietre/CsQuery

            CQ _dom = dom;
            CQ _elements;
            if (!string.IsNullOrEmpty(selector))
                _elements = _dom[selector];
            else
                _elements = _dom;

            var _result = new List<string>();

            foreach (var _element in _elements)
            {
                // TODO: 7.29.2014 - Move to Strategy pattern
                if (getValueMethod == GetValueMethods.Html)
                    _result.Add(_element.InnerHTML.Trim());
                else if (getValueMethod == GetValueMethods.Text)
                    _result.Add(_element.InnerText.Trim());
                else if (getValueMethod == GetValueMethods.AttributeHref)
                    _result.Add(_element.GetAttribute("href"));
                else if (getValueMethod == GetValueMethods.AttributeSrc)
                    _result.Add(_element.GetAttribute("src"));
            }

            return _result;
        }
    }
}