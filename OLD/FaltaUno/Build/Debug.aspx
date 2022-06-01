<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Debug.aspx.cs" Inherits="WebTest.Debug1" %>

<html>
<head>
   <style src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" type="text/css"></style>


</head>
<body>
   <div class="row">
      <div class="col-xs-12">
         Web service path<br />
         <input name="ws" id="ws" value="DebugWS.aspx"/>
      </div>
   </div>
   <div class="row">
      <div class="col-xs-12">
         Method Name in DebugWS<br />
         <input name="methodName" id="methodName" value=""/>
      </div>
   </div>

   <div class="row">
      <div class="col-xs-12">
         Success<br /><br />
         <div id="success"> Success result here </div>
      </div>
   </div>
   <div class="row">
      <div class="col-xs-12">
         Error<br /><br />
         <div id="error"> Error result here </div>
      </div>
   </div>

    <div class="row">
      <div class="col-xs-12">
         <button>Send ajax request</button>
      </div>
   </div>

   <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js" type="text/javascript"></script>
   <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" type="text/javascript"></script>

<script>
$(document).ready(function(){
    $("button").click(function(){
       $.ajax({
          url: $("#ws").val() + (($("#methodName").val()!="")?("?Method=" + $("#methodName").val()):""),
          success: function (result) {
            $("#success").html(result);
          },
          error: function (result) {
            $("#error").html(result);
          }
       });
    });
});
</script>
</body>
</html>
