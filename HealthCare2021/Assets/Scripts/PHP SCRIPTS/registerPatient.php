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

    // check if phone number exists
    $phoneNumberCheckQuery = "SELECT phoneNumber FROM users WHERE phoneNumber=' " . $phoneNumber . "';";

    $phoneNumberCheck = mysqli_query($con, $phoneNumberCheckQuery) or die("2: name check query failed"); //error code #2 - name check query failed

    if(mysqli_num_rows($phoneNumberCheck) > 0)
    {
        echo "3: name already exists"; //error code #3 - name exists cannot register
    }

    else
    {
        // Add user to the table
        $salt = "\$5\$rounds=5000\$" . "steamedhams" . $phoneNumber . "\$";
        $hash = crypt($password, $salt);

        function generateUniquePatientID($con) {
            $randID = rand(100000000, 999999999);
            $checkQuery = "SELECT patientID FROM users WHERE patientID='" . $randID . "';";
            $checkResult = mysqli_query($con, $checkQuery) or die("Error: Unable to check patientID uniqueness");
            
            if (mysqli_num_rows($checkResult) > 0) {
                // If the generated patientID already exists, generate a new one recursively
                return generateUniquePatientID($con);
            } else {
                // If the generated patientID is unique, return it
                return $randID;
            }
        }

        // Generate a unique patientID
        $randID = generateUniquePatientID($con);

        $insertUserQuery = "INSERT INTO users (phoneNumber, hash, salt, patientID) VALUES ('" . $phoneNumber . "' , '" . $hash . "' , '" . $salt . "' , '" . $randID . "');";

        $result = mysqli_query($con, $insertUserQuery);

        if ($result)
        {
            echo "0"; // Registration successful
        }
        else
        {
            echo "4: registration failed"; //error code #4 - registration failed
        }
    }
    mysqli_close($con);
?>