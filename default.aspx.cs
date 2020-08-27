using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web.Services.Description;
using RestSharp;
using System.Data;
using System.Threading;

namespace webservice_livsaude
{
    public partial class _default : System.Web.UI.Page
    {

        public class JsonLiv
        {
            public string nome_plano { get; set; }
            public string codigo { get; set; }
            public string beneficiario { get; set; }
            public string validade { get; set; }
            public string dt_nascimento { get; set; }
            public string segmentacao { get; set; }
            public string acomodacao { get; set; }
            public string dt_inclusao { get; set; }
            public string cpt { get; set; }
            public string cns { get; set; }
            public string empresa { get; set; }
            public List<Carencia> carencias { get; set; }
            public string cep { get; set; }
            public string endereco { get; set; }
            public string numero { get; set; }
            public string bairro { get; set; }
            public string complemento { get; set; }
            public string cidade { get; set; }
            public string estado { get; set; }
            public string cpf { get; set; }
            public string numero_familia { get; set; }
            public string tipo_usuario { get; set; }
            public string numero_contrato { get; set; }

        }

        public class Carencia
        {
            public string vencimento { get; set; }
            public string descricao { get; set; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ArqLivJason(string arqJason, string nomarquivo)
        {
            try
            {
                    bool primeiro = true;
                string numeroFamilia = "";
                int contFamilia = 0;
                int contCartao = 0;
                          
                //StreamReader reader = new StreamReader(arqJason);
                string rawSendGridJSON = arqJason;
                List<JsonLiv> livEvents = JsonConvert.DeserializeObject<List<JsonLiv>>(rawSendGridJSON);
                string count = livEvents.Count.ToString();

                //CAMINHO SERVIDOR
                string dir = Server.MapPath("/livsaude/repositorio");

                //CAMINHO LOCAL
                //string dir = Server.MapPath("/repositorio");

                DirectoryInfo directoryInfo = new DirectoryInfo(dir);

                System.DateTime agora = System.DateTime.Now;

                string directoryname = directoryInfo.FullName;

                //System.Diagnostics.Trace.TraceError(rawSendGridJSON + directoryname + "logSendGrid" + agora.ToString("ddMMyyyy") + ".txt"); // For debugging to the Azure Streaming logs

                JsonLiv Events = new JsonLiv();

                try
                {

                    foreach (JsonLiv LivLiveEvent in livEvents)
                    {

                        //Events.nome_plano = LivLiveEvent.nome_plano;
                        //Events.codigo = LivLiveEvent.codigo;
                        //Events.beneficiario = LivLiveEvent.beneficiario;
                        //Events.validade = LivLiveEvent.validade;
                        //Events.dt_nascimento = LivLiveEvent.dt_nascimento;
                        //Events.segmentacao = LivLiveEvent.segmentacao;
                        //Events.acomodacao = LivLiveEvent.acomodacao;
                        //Events.dt_inclusao = LivLiveEvent.dt_inclusao;
                        //Events.cpt = LivLiveEvent.cpt;
                        //Events.cns = LivLiveEvent.cns;
                        //Events.empresa = LivLiveEvent.empresa;
                        //Events.cep = LivLiveEvent.cep;
                        //Events.bairro = LivLiveEvent.bairro;
                        //Events.complemento = LivLiveEvent.complemento;
                        //Events.cidade = LivLiveEvent.cidade;
                        //Events.estado = LivLiveEvent.estado;
                        //Events.cpf = LivLiveEvent.cpf;
                        //Events.numero_familia = LivLiveEvent.numero_familia;

                        //List<string> listaCarencia = JsonConvert.DeserializeObject<List<string>>(LivLiveEvent.carencias);
                        int contcarencia = 0;
                        string carencia = "";

                        for (int i = 0; i < LivLiveEvent.carencias.Count(); i++)
                        {
                            contcarencia++;
                            carencia = carencia + LivLiveEvent.carencias[i].descricao + ";" + LivLiveEvent.carencias[i].vencimento + ";";

                        }

                        int numcarencia = LivLiveEvent.carencias.Count();

                        if (numcarencia % 7 == 0)
                        {
                        }
                        else
                        {
                            int resto = 7 - numcarencia % 7;

                            for (int a = 0; a < resto; a++)
                            {
                                contcarencia++;
                                carencia = carencia + ";" + ";";

                            }

                        }

                        //CONTFAMILIA E CAMPO 1 E 3
                        if (primeiro == true)
                        {
                            primeiro = false;

                            numeroFamilia = LivLiveEvent.numero_familia;

                            using (StreamWriter writer = new StreamWriter(directoryname + @"\" + nomarquivo + ".csv", true))
                            {
                                writer.WriteLine("0;" + LivLiveEvent.empresa + ";;;;;;;");
                                writer.WriteLine("A;" + LivLiveEvent.numero_familia);
                            }
                        }
                        else
                        {
                            if (numeroFamilia != LivLiveEvent.numero_familia)
                            {

                                using (StreamWriter writer = new StreamWriter(directoryname + @"\" + nomarquivo + ".csv", true))
                                {
                                    writer.WriteLine("3;" + contFamilia);
                                    writer.WriteLine("A;" + LivLiveEvent.numero_familia);
                                }

                                numeroFamilia = LivLiveEvent.numero_familia;
                                contFamilia = 0;
                            }

                        }

                        //PRINCIPAL
                        using (StreamWriter writer = new StreamWriter(directoryname + @"\" + nomarquivo + ".csv", true))
                        {
                            //if (LivLiveEvent.tipo_usuario == "TITULAR")
                            //{

                            writer.WriteLine("1;" + LivLiveEvent.nome_plano + ";" + LivLiveEvent.codigo + ";" + LivLiveEvent.beneficiario + ";" + LivLiveEvent.validade + ";" + LivLiveEvent.dt_nascimento + ";" + LivLiveEvent.segmentacao + ";" + LivLiveEvent.acomodacao + ";" + LivLiveEvent.dt_inclusao + ";" + LivLiveEvent.cpt + ";" + LivLiveEvent.cns + ";" + LivLiveEvent.empresa + ";" + carencia + LivLiveEvent.cep + ";" + LivLiveEvent.endereco + ";" + LivLiveEvent.numero + ";" + LivLiveEvent.bairro + ";" + LivLiveEvent.complemento + ";" + LivLiveEvent.cidade + ";" + LivLiveEvent.estado + ";" + LivLiveEvent.cpf + ";" + LivLiveEvent.numero_familia + ";" + LivLiveEvent.tipo_usuario + ";" + LivLiveEvent.numero_contrato);
                            contFamilia++;
                            contCartao++;

                            //}
                            //else
                            //{

                            //    writer.WriteLine("2;" + LivLiveEvent.nome_plano + ";" + LivLiveEvent.codigo + ";" + LivLiveEvent.beneficiario + ";" + LivLiveEvent.validade + ";" + LivLiveEvent.dt_nascimento + ";" + LivLiveEvent.segmentacao + ";" + LivLiveEvent.acomodacao + ";" + LivLiveEvent.dt_inclusao + ";" + LivLiveEvent.cpt + ";" + LivLiveEvent.cns + ";" + LivLiveEvent.empresa + ";" + carencia + LivLiveEvent.cep + ";" + LivLiveEvent.endereco + ";" + LivLiveEvent.numero + ";" + LivLiveEvent.bairro + ";" + LivLiveEvent.complemento + ";" + LivLiveEvent.cidade + ";" + LivLiveEvent.estado + ";" + LivLiveEvent.cpf + ";" + LivLiveEvent.numero_familia + ";" + LivLiveEvent.tipo_usuario + ";" + LivLiveEvent.numero_contrato);
                            //    contFamilia++;

                            //}

                        }

                    }

                    //ultima linha

                    using (StreamWriter writer = new StreamWriter(directoryname + @"\" + nomarquivo + ".csv", true))
                    {
                        writer.WriteLine("3;" + contFamilia);
                        writer.WriteLine("9;" + contCartao);
                    }
                                        
                    System.IO.FileInfo arquivo = new System.IO.FileInfo(directoryname + @"\" + nomarquivo + ".csv");
                  
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + arquivo.Name);
                    Response.AddHeader("Content-Length", arquivo.Length.ToString());
                    Response.ContentType = "text/plain";
                    Response.Flush();
                    Response.TransmitFile(arquivo.FullName);
                    Response.End();
                    
                    System.IO.File.Delete(arquivo.FullName);

                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);

                }
                catch (Exception ex)
                {
                    lblerror.Text = ex.ToString();

                    using (StreamWriter writer = new StreamWriter(directoryname + @"\" + "ErrorSendGrid" + nomarquivo + ".csv", true))
                    {
                        writer.Write(ex.ToString());
                    }

                }



            }
            catch (Exception ex)
            {
                lblerror.Text = ex.ToString();
            }

        }


        protected void Button1_Click1(object sender, EventArgs e)
        {

            try
            {

                string[] linhaTextBox = TextBox1.Text.Split(new String[] { "\r\n" }, System.StringSplitOptions.None);

                string cd_remessa, cd_criacao = null;
                string[] linhaseparada = null;
                List<Object> linhas = new List<Object>();
                int intcontador = 0;

                cd_remessa = "";
                cd_criacao = "";

                // ler o conteudo da linha e add na list 
                foreach (string linha in linhaTextBox)
                {
                    intcontador = 0;

                    linhaseparada = linha.Split(';');
                    foreach (var item in linhaseparada)
                    {
                        intcontador++;

                        switch (intcontador)
                        {
                            case 1:
                                cd_remessa = item.Replace(".", "");
                                break;
                            case 2:
                                cd_criacao = item;

                                if (cd_criacao != "Data geração")
                                {
                                    var client = new RestClient("https://livtech.livsaude.com.br/livnow/app/carteiras/lote.php");
                                    var request = new RestRequest(Method.POST);
                                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                                    //request.AddHeader("authorization", "Bearer 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");
                                    request.AddParameter("cd_remessa", cd_remessa);
                                    request.AddParameter("cd_criacao", cd_criacao);
                                    IRestResponse response = client.Execute(request);

                                    string arqJason = response.Content;
                                    ArqLivJason(arqJason, cd_remessa);

                                }

                                break;

                        }

                    }
                }


            }
            catch (Exception ex)
            {
                lblerror.Text = ex.ToString();

            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            if (FileUpload1.HasFile && Path.GetExtension(FileUpload1.FileName) == ".csv")
            {
                using (StreamReader arquivo = new StreamReader(FileUpload1.PostedFile.InputStream))
                {

                    try
                    {

                        int codigo = 0;
                        int data = 0;
                        int lin = 0;

                        string linha, cd_remessa, cd_criacao = null;
                        string[] linhaseparada = null;
                        List<Object> linhas = new List<Object>();
                        int intcontador = 0;

                        cd_remessa = "";
                        cd_criacao = "";

                        // ler o conteudo da linha e add na list 
                        while ((linha = arquivo.ReadLine()) != null)
                        {
                            lin++;

                            intcontador = 0;

                            linhaseparada = linha.Split(';');

                            if (lin == 1)
                            {

                                foreach (var item in linhaseparada)
                                {
                                    intcontador++;

                                    switch (item.ToUpper())
                                    {

                                        case "CODIGO":
                                            codigo = intcontador;
                                            break;
                                        case "DATA":
                                            data = intcontador;
                                            break;

                                    }

                                }

                            }

                            intcontador = 0;


                            foreach (var item in linhaseparada)
                            {
                                intcontador++;

                                if (intcontador == codigo)
                                {
                                    cd_remessa = item.Replace(".", "");
                                }

                                if (intcontador == data)
                                {
                                    cd_criacao = item;

                                    if (cd_criacao != "DATA")
                                    {
                                        var client = new RestClient("https://livtech.livsaude.com.br/livnow/app/carteiras/lote.php");
                                        var request = new RestRequest(Method.POST);
                                        request.AddHeader("content-type", "application/x-www-form-urlencoded");
                                        //request.AddHeader("authorization", "Bearer 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");
                                        request.AddParameter("cd_remessa", cd_remessa);
                                        request.AddParameter("cd_criacao", cd_criacao);
                                        IRestResponse response = client.Execute(request);

                                        string arqJason = response.Content;
                                        ArqLivJason(arqJason, cd_remessa);

                                    }
                                }

                            }
                        }


                    }
                    catch
                    {

                    }

                }
            }
            else
            {
                UploadStatusLabel.Text = "O arquivo precisa ser csv separado por ponto e vírgula.";
            }
        }
    }
}