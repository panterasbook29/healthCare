<?php

$con = mysqli_connect('192.168.0.100', 'pantera1_alsivic', '81(wE(pzS9', 'pantera1_alsivic');
//check that connection happened
if(mysqli_connect_errno())
{
    echo "1: connection failed"; //error code #1 = connection failed
    exit();
}

$phoneNumber = $_POST["phoneNumber"];
$password = $_POST["password"];

$phoneNumberCheckQuery = "SELECT phoneNumber, salt, hash FROM users WHERE phoneNumber='". $phoneNumber ."';";

$phoneNumberCheck = mysqli_query($con, $phoneNumberCheckQuery) or die("2: phoneNumber check query failed"); //error code #2 - name check query failed

if(mysqli_num_rows($phoneNumberCheck) != 1)
{
    //echo $codeCheckQuery;
    echo "5: Either no user with phoneNumber or more than one"; // error code #5 - number of phoneNumbers matching != 1
    exit();
}

//get login info from query
$existingInfo = mysqli_fetch_assoc($phoneNumberCheck);
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