<?php
if(/*isset($_GET['cod']) && */isset($_GET['ids']))
{
    //$cod = $_GET['cod'];
    $ids = $_GET['ids'];

    $server = 'localhost';
    $user = 'Logikoz';
    $pass = '123';
    $db = 'projects';
	#endregion

    try
    {
        $pdo = new PDO("mysql:host=$server; dbname=$db; charset=utf8;", $user, $pass);

        $post = $pdo->prepare("select produto_desc from clientes where binary id_software = ?" /*+ cod_cliente = ? && + */);
        $post -> bindParam(1, $ids);
        //$post -> bindParam(2, $cod);
        if($post ->execute() && $row = $post->fetch())
        {
            echo $row['produto_desc'];
        }
        else
        {
            echo "no";
        }
    }
    catch(PDOException  $e )
    {
        echo "Error: ".$e;
    }
}
else
{
    echo "Nao pode deixar nenhum dos campos vazios!";
}
?>