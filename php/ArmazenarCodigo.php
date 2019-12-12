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

		$inserirCodigo = $con -> prepare("insert into codigosrecuperar (id, codigo, email) values (null, ?, ?)");
		$inserirCodigo -> bindParam(1, $codigo);
		$inserirCodigo -> bindParam(2, $email);

		$emailExiste = $con -> prepare("select count(*) from usuarios where email_user = ?");
		$emailExiste -> bindParam(1, $email);

		if($emailExiste -> execute() && $emailExiste -> fetchColumn() > 0)
		{
			if($inserirCodigo -> execute())
			{
				echo "SUCESSO";
			}
			else
			{
				echo "ERRO_AO_INSERIR";
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