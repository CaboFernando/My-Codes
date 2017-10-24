public string ÉcachaçaCarai(String data) {

            if (data.Length < 10)
                return null;

            string novoDia = data.Substring(0, 2);
            string novoMes = data.Substring(3, 2);
            string novoAno = data.Substring(6, 4);
            string novaData = novoAno + "-" + novoMes + "-" + novoDia;
            
            return novaData;
        }

        public void ImportarInsertSGC(String arquivo = "D:\\files.csv", String planilha = "files") {

            List<String> insertsPropostaCartaGarantia = new List<String>();
            List<String> AlgumErro = new List<String>();
            List<String> ids_associados = new List<String>();
            List<DataTable> results = new List<DataTable>();
            String id_associado, data_criacao, data_analista, data_comite, proposta_forlogic, codigo_carta, data_emissao, data_geracao, data_credito, operacao, operacao_aprovado, modalidade, porcent_gar, porcent_gar_aprov, carta_fianca, carta_fia_aprov, id_if, id_carta, valor_comissao, prazo, prazo_aprv, id_estado_propos;


            var reader = new System.IO.StreamReader(System.IO.File.OpenRead(arquivo));
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            List<string> listC = new List<string>();
            List<string> listD = new List<string>();
            List<string> listE = new List<string>();
            List<string> listF = new List<string>();
            List<string> listG = new List<string>();
            List<string> listH = new List<string>();
            List<string> listI = new List<string>();
            List<string> listJ = new List<string>();
            List<string> listK = new List<string>();
            List<string> listL = new List<string>();
            List<string> listM = new List<string>();
            List<string> listN = new List<string>();
            List<string> listO = new List<string>();
            List<string> listP = new List<string>();
            List<string> listQ = new List<string>();
            List<string> listR = new List<string>();

            while (!reader.EndOfStream) {
                var line = reader.ReadLine();
                var values = line.Split(';');
                listA.Add(values[0].Replace("-",".","/",""));
                listB.Add(values[1]);
                listC.Add(values[2]);
                listD.Add(values[3]);
                listE.Add(values[4]);
                listF.Add(values[5]);
                listG.Add(values[6]);
                listH.Add(values[7].Replace(".",""));
                listI.Add(values[8]);
                listJ.Add(values[9].Replace("%",""));
                listK.Add(values[10].Replace(".", ""));
                listL.Add(values[11]);
                listM.Add(values[12]);
                listN.Add(values[13].Replace(".", ""));
                listO.Add(values[14]);
                listP.Add(values[15]);
                listQ.Add(values[16]);
                listR.Add(values[17]);
            }
           
            var db = new Db();
                        
            for ( int i = 0; i < 1559; i++) {
                results.Add(db.Select(new Sql("select a.id from dbo.pessoas_juridicas pj join dbo.associados a on (a.id_pessoa_juridica = pj.id) where id_unidade = 9 and ((pj.cnpj like ?) or (pj.razao_social like ?) )", listA[i].Trim(), "%" + listQ[i].Trim() + "%")));

                try {
                    if (results[i].Rows[0] != null) {
                        ids_associados.Add(results[i].Rows[0]["id"].ToString());
                        continue;
                    }
                    ids_associados.Add("null");
                } catch (IndexOutOfRangeException) {
                    ids_associados.Add("null");
                    AlgumErro.Add("!"+listA[i]+ "!");
                    continue;
                    throw;
                }                
            }
            
            string filePath1 = "D:\\Erros.csv";

            if (!System.IO.File.Exists(filePath1)) {
                System.IO.File.Create(filePath1).Close();
            }
            string delimter1 = ",";

            int length1 = AlgumErro.Count;

            using (System.IO.TextWriter writer = System.IO.File.CreateText(filePath1)) {
                for (int index = 0; index < length1; index++) {
                    writer.WriteLine(string.Join(delimter1, AlgumErro[index]));
                }
            }
            
            for (int i = 0; i < 1559; i++) {
                id_associado = (ids_associados[i] == "" ? "null" : ids_associados[i]);
                data_criacao = (listB[i] == "" ? "null" : "'"+ ÉcachaçaCarai(listB[i])+"'");
                data_analista = (listC[i] == "" ? "null" : "'"+ ÉcachaçaCarai(listC[i])+"'");
                data_comite = (listD[i] == "" ? "null" : "'"+ ÉcachaçaCarai(listD[i])+"'");
                proposta_forlogic = (listB[i] == "" ? "null" : "'" + listB[i].Substring(6) + "/" + i + "'");
                codigo_carta = (listE[i] == "" ? "null" : "'" + listE[i] + "'");
                data_emissao = (listF[i] == "" ? "null" : "'"+ ÉcachaçaCarai(listF[i])+"'");
                data_geracao = (listF[i] == "" ? "null" : "'"+ ÉcachaçaCarai(listF[i])+"'");
                data_credito = (listG[i] == "" ? "null" : "'"+ ÉcachaçaCarai(listG[i])+"'");
                operacao = (listH[i] == "" ? "null" : listH[i].Replace(",", "."));
                operacao_aprovado = (listH[i] == "" ? "null" : listH[i].Replace(",", "."));
                modalidade = (listI[i] == "" ? "null" : listI[i]);
                porcent_gar = (listJ[i] == "" ? "null" : listJ[i]);
                porcent_gar_aprov = (listJ[i] == "" ? "null" : listJ[i]);
                carta_fianca = (listK[i] == "" ? "null" : listK[i].Replace(",", "."));
                carta_fia_aprov = (listK[i] == "" ? "null" : listK[i].Replace(",", "."));
                id_if = (listL[i] == "" ? "null" : listL[i]);
                id_carta = (listM[i] == "" ? "null" : listM[i]);
                valor_comissao = (listN[i] == "" ? "null" : listN[i].Replace(",", "."));
                prazo = (listO[i] == "" ? "null" : listO[i]);
                prazo_aprv = (listO[i] == "" ? "null" : listO[i]);
                id_estado_propos = (listP[i] == "" ? "null" : listP[i]);                


                if (!listR.Contains(codigo_carta.Replace("'", ""))) {
                    insertsPropostaCartaGarantia.Add("insert into propostas (id_associado, codigo, data_criacao, data_analista, data_comite, data_credito, operacao_aprovado, operacao, id_modalidade, porcentagem_garantia, porcentagem_garantia_aprovado, carta_fianca, carta_fianca_aprovado, id_instituicao_financeira, id_carta, valor_comissao_garantia, prazo, prazo_aprovado, id_estado_proposta, id_unidade) values (" + id_associado + "," + proposta_forlogic + "," + data_criacao + "," + data_analista + "," + data_comite + "," + data_credito + "," + operacao_aprovado + "," + operacao + "," + modalidade + "," + porcent_gar + "," + porcent_gar_aprov + "," + carta_fianca + "," + carta_fia_aprov + "," + id_if + "," + id_carta + "," + valor_comissao + "," + prazo + "," + prazo_aprv + "," + id_estado_propos + "," + 9 + ")\ngo");
                    if (codigo_carta != "null") {
                        insertsPropostaCartaGarantia.Add("insert into cartas_garantia (codigo, data_geracao_carta, id_proposta, removido, data_emissao_carta) values (" + codigo_carta + "," + data_geracao + ", (select id from propostas where codigo = " + proposta_forlogic + ") ," + 0 + "," + data_emissao + ")\ngo\n");
                    }
                }
            }

            string filePath = "D:\\inserts.csv";

            if (!System.IO.File.Exists(filePath)) {
                System.IO.File.Create(filePath).Close();
            }
            string delimter = ",";

            int length = insertsPropostaCartaGarantia.Count;

            using (System.IO.TextWriter writer = System.IO.File.CreateText(filePath)) {
                for (int index = 0; index < length; index++) {
                    writer.WriteLine(string.Join(delimter, insertsPropostaCartaGarantia[index]));
                }
            }

            if (true) { /* Terminou!!!! */}            
        }