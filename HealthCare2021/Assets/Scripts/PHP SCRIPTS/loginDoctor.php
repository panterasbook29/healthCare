<?php

$con = mysqli_connect('192.168.0.100', 'pantera1_alsivic', '81(wE(pzS9', 'pantera1_alsivic');
//check that connection happened
if(mysqli_connect_errno())
{
    echo "1: connection failed"; //error code #1 = connection failed
    exit();
}

$cnp = $_POST["cnp"];
$password = $_POST["password"];

$cnpCheckQuery = "SELECT cnp, salt, hash FROM doctors WHERE cnp='". $cnp ."';";

$cnpCheck = mysqli_query($con, $cnpCheckQuery) or die("2: cnp check query failed"); //error code #2 - name check query failed

if(mysqli_num_rows($cnpCheck) != 1)
{
    //echo $cnpCheckQuery;
    echo "5: Either no user with cnp or more than one"; // error code #5 - number of cnps matching != 1
    exit();
}

//get login info from query
$existingInfo = mysqli_fetch_assoc($cnpCheck);
$salt = $existingInfo["salt"];
$hash = $existingInfo["hash"];

$loginHash = crypt($password, $salt);
if($hash != $loginHash)
{
    echo "6: Incorrect Password"; // error code #6 : password does not hash to match the table
    exit();
}
echo "0\t" ; //. $existingInfo[]

?>