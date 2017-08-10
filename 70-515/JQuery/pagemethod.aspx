<%@ Page Language="C#" %>
<%@ Import Namespace="System.Web.Services" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html  >
<head id="Head1" runat="server">
<title>ASP.NET AJAX Web Services: Web Service Sample Page</title>

   <script type="text/javascript"  src="http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js">   
</script>  

    <script runat="server">
        
        [WebMethod()]
        public static string sayHello()
        {
            return "hello ";
        }  

        </script>

  <script type="text/javascript">

      $(document).ready(function() {
          $.ajax({
              type: "POST",
              url: "pagemethod.aspx/sayHello",
              contentType: "application/json; charset=utf-8",
              data: "{}",
              dataType: "json",
              success: AjaxSucceeded,
              error: AjaxFailed
          }); 

    });

      function AjaxSucceeded(result) {
          alert(result.d);
      }
      function AjaxFailed(result) {
          alert(result.status + ' ' + result.statusText);
      }  
  </script>  
</head>

<body>
    <form id="form1" runat="server">

        <div>


        </div>

    </form>
</body>
</html>
