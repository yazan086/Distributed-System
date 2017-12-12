﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>The Greatest Simulator Ever</title>
    <link rel="stylesheet" href="css/style.css" type="text/css" />

    <script type="text/javascript" src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>
    <script src="js/screenShot.js"></script>

</head>
<body>

	

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
		<asp:FileUpload ID="FileUpload" runat="server" style="display:none" />
		<asp:Button runat="server" ID="UploadButton" style="display:none" Text="Upload" OnClick="UploadFile" />
		

        <div class="header">SEQUENCE DIAGRAM SIMULATOR</div>
        <div class="left">
            <div class="content3">
                Device List:<br/> <br/>
            </div>
            <div class="content3" id="uploads">Uploaded Files
                <asp:UpdatePanel ID="pnlUploads" runat="server">

                </asp:UpdatePanel>
            </div>
            <span id="mCount"></span>
            <div class="content3" id="nCount">Name of Nodes</div>
            
            
        </div>
        <div class="container">

            <asp:Button runat="server" ID="btnSend" Style="display: none" OnClick="send" />
            <asp:Button runat="server" ID="btnRestart" Style="display: none" OnClick="restartProcess" />

            <div class="nav">
                <ul>
                    <li>
                        <div class="upload" onclick="document.getElementById('<%= FileUpload.ClientID %>').click()"></div>
                    </li>
                    <li>
                        <div class="start" onclick="document.getElementById('<%= btnSend.ClientID %>').click()"></div>
                    </li>
                    <li>
                        <div class="pause"></div>
                    </li>
                    <li>
                        <div class="stopReset" onclick="document.getElementById('<%= btnRestart.ClientID %>').click()"></div>
                    </li>
                    <li>
                        <div class="capture" onclick="screenShot()"></div>
                    </li>
                    <li>
                        <div class="send"></div>
                    </li>
                </ul>
            </div>
            <div class="sd">
                <canvas id="SDCanvas"></canvas>
            </div>
        </div>
        
        <div class="console">console log here</div>
        <script type="text/javascript" src="js/parse.js"></script>
        <script src="js/simulator.js"></script>
        <script type="text/javascript">
             function UploadFile(fileUpload) {
                 if (fileUpload.value != '') {
                     document.getElementById("<%=UploadButton.ClientID %>").click();
                 }
             }
        </script>

    </form>

</body>
</html>