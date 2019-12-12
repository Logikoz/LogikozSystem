<?php

if(isset($_GET['cod']) && isset($_GET['email']))
{
	$codigo = $_GET['cod'];
	$email = $_GET['email'];

    $server = 'localhost';
    $user = 'Logikoz';
    $pass = '123';
    $db = 'projects';

	try
	{
		$con = new PDO("mysql:host=$server; dbname=$db; charset=utf8;", $user, $pass);

		$codigoExiste = $con -> prepare("select count(*) from codigosrecuperar where codigo = ? && email = ?");
		$codigoExiste -> bindParam(1, $codigo);
		$codigoExiste -> bindParam(2, $email);

		$emailExiste = $con -> prepare("select count(*) from usuarios where email_user = ?");
		$emailExiste -> bindParam(1, $email);

		if($emailExiste -> execute() && $emailExiste -> fetchColumn() > 0)
		{
			if($codigoExiste -> execute() && $codigoExiste -> fetchColumn() > 0)
			{
				echo "SUCESSO";
			}
			else
			{
				echo "CODIGO_NAO_EXISTE";
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