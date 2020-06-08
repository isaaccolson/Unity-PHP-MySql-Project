<?php

function parsedate(string $date) {
    return substr($date, -4) . "-" . substr($date, 0,2) . "-" . substr($date, 3,2);
}

  $conn = mysqli_connect('localhost', 'root', 'root', 'unity_access');

  //check to see if the conn happened
  if (mysqli_connect_errno())
  {
    echo "1: Connection failed";
    exit();
  }

  //get the fields from the form
  $username = $_POST["name"];
  $datestring = $_POST["dob"];
  $dob = parsedate($datestring);
  $comments = $_POST["comments"];

  //check if name exists
  $namecheckquery = "SELECT name FROM subjects WHERE name = '" . $username . "';";
  $namecheck = mysqli_query($conn, $namecheckquery) or die("2: Name check query failed");

  if (mysqli_num_rows($namecheck) > 0)
  {
    die("3: Name already exists");
  }

  $subjectinsertquery = "INSERT INTO subjects (name, dob, comments) VALUES ('" . $username . "','" . $dob . "','" . $comments . "');";
  mysqli_query($conn, $subjectinsertquery) or die("4: Insert into DB failed.");

  //want to return the user's id for the next part of the test
  $idcheckquery = "SELECT id FROM subjects WHERE name = '" . $username . "';";
  $idcheck = mysqli_query($conn, $idcheckquery) or die("5: Id check query failed");

  while($row = mysqli_fetch_assoc($idcheck)) {
               die("0:" . $row["id"]);
            }

  //if (mysqli_num_rows($idcheck) > 0)
  //{
    //die("0: Id:");
  //}
  die("0: Sucessfully inserted " . $username);


 ?>
