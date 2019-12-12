<?php

if(isset($_GET['user']) && isset($_GET['pwd']))
{
	$usuario = $_GET['user'];
	$senha = $_GET['pwd'];

    $server = 'localhost';
    $user = 'Logikoz';
    $pass = '123';
    $db = 'projects';

	try
	{
		$con = new PDO("mysql:host=$server; dbname=$db; charset=utf8;", $user, $pass);


		$post = $con -> prepare("select count(*) from usuarios where login_user = ? && senha_user = ?");
		$post -> bindParam(1, $usuario);
		$post -> bindParam(2, $senha);

		if($post -> execute() && $post -> fetchColumn() > 0)
		{
			echo "YES";
		}
		else
		{
			echo "NO";
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