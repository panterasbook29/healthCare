<?php

$con = mysqli_connect('192.168.0.100', 'pantera1_alsivic', '81(wE(pzS9', 'pantera1_alsivic');

// Check if the connection happened
if (mysqli_connect_errno()) {
    echo "1: connection failed"; // error code #1 = connection failed
    exit();
}

$cnp = $_POST["cnp"];
$password = $_POST["password"];

// Check if the cnp exists
$cnpCheckQuery = "SELECT cnp FROM doctors WHERE cnp='" . $cnp . "';";

$cnpCheck = mysqli_query($con, $cnpCheckQuery) or die("2: cnp check query failed"); // error code #2 - cnp check query failed

if (mysqli_num_rows($cnpCheck) > 0) 
{
    // cnp exists, update the existing record
    $salt = "\$5\$rounds=5000\$" . "steamedhams" . $cnp . "\$";
    $hash = crypt($password, $salt);

    $updateUserQuery = "UPDATE doctors SET hash='" . $hash . "', salt='" . $salt . "' WHERE cnp='" . $cnp . "';";

    $result = mysqli_query($con, $updateUserQuery);

    if ($result) {
        echo "0"; // Update successful
    } else {
        echo "5: update failed"; // error code #5 - update failed
    }
} else {
    // cnp doesn't exist, handle accordingly
    echo "6: cnp does not exist"; // error code #6 - cnp does not exist
}

mysqli_close($con);
?>
