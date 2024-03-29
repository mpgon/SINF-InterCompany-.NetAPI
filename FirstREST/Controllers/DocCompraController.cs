﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;

namespace FirstREST.Controllers
{
    public class DocCompraController : ApiController
    {


        public IEnumerable<Lib_Primavera.Model.DocCompra> Get(string codEmpresa, string tipoDeDocumento)
        {
            return Lib_Primavera.Comercial.ListaDocumentosCompra(codEmpresa, tipoDeDocumento);
        }


        public Lib_Primavera.Model.DocCompra Get(string codEmpresa, string tipoDeDocumento, string numDoc)
        {
            Lib_Primavera.Model.DocCompra doccompra = Lib_Primavera.Comercial.GetDocumentoCompra(codEmpresa, tipoDeDocumento, numDoc);
            if (doccompra == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));

            }
            else
            {
                return doccompra;
            }
        }



        public HttpResponseMessage Post(string id, Lib_Primavera.Model.DocCompra dc)
        {
            //Console.Write("Documento de compra: \n");
           // Console.Write(dc);
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.Comercial.NovoDocumentoCompra(id, dc);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, dc.id);
                string uri = Url.Link("DefaultApi", new { DocId = dc.id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }

    }
}
