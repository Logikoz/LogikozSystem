using System;

namespace LogikozSystem._Dep
{
	class PegarInfos
	{
		public static string Url => $"localhost/{string.Concat(Nome, SobreNome)}";
		public static string Nome => "Logikoz";
		public static string SobreNome => "System";
		public static string Versao => "1.5";
		public static string CaminhoEmailTemplate => "./EmailSend/SolicitarSenha.html";
	}
}
