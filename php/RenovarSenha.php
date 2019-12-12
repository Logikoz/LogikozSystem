<?php

if(isset($_GET['cod']) && isset($_GET['email']) && isset($_GET['newpwd']))
{
	$codigo = $_GET['cod'];
	$email = $_GET['email'];
	$novaSenha = $_GET['newpwd'];

    $server = 'localhost';
    $user = 'Logikoz';
    $pass = '123';
    $db = 'projects';

	try
	{
		$con = new PDO("mysql:host=$server; dbname=$db; charset=utf8;", $user, $pass);

		$emailExiste = $con -> prepare("select count(*) from usuarios where email_user = ?");
		$emailExiste -> bindParam(1, $email);
		
		$aplicarNovaSenha = $con -> prepare("update usuarios set senha_user = ? where email_user = ?");
		$aplicarNovaSenha -> bindParam(1, $novaSenha);
		$aplicarNovaSenha -> bindParam(2, $email);
		
		$deletarCodigo = $con -> prepare("delete from codigosrecuperar where codigo = ? && email = ?");
		$deletarCodigo -> bindParam(1, $codigo);
		$deletarCodigo -> bindParam(2, $email);

		if($emailExiste -> execute() && $emailExiste -> fetchColumn() > 0)
		{
				if($aplicarNovaSenha -> execute())
				{
					$deletarCodigo -> execute();
					echo "SUCESSO";
				}
				else
				{
					echo "ERRO_AO_MUDAR_SENHA";
				}
		}
		else
		{
			echo "EMAIL_NAO_EXISTE";
		}
	}
	catch (PDOException $e)
	{
		echo "Erro: " . $e;
	}
}
else
{
	echo "Voce precisa informar todos os valores! :D";
}

?>