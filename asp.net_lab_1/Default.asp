<%@  language="JavaScript" %>
<html>
<body>
	<% 
		var ForReading = 1, ForAdding = 8;

		var abs_path = String(Request.ServerVariables("PATH_TRANSLATED")); 
		var file_to_read = abs_path.replace(/\\\w*\.asp/,"\\")+"test.txt";
		var file = new ActiveXObject("Scripting.FileSystemObject");

		var strChoise = String(Request.Form("choise"));
		var strName= String(Request.Form("name"));

		var date = new Date();
		date.setDate(date.getDate() + 7);

		if (strChoise == "undefined" || strName == "undefined" || strName.length == 0) 
		{
	%>
	<form method="post" action="Default.asp">
		<font>Введите имя</font>
		<input name="name" type="text" value="<%strName%>" />
		<br />
		<font>Выберите товар</font>
		<select name="choise">
			<% 	
				var openFile = file.OpenTextFile(file_to_read, ForReading, false);
				while (!openFile.AtEndOfStream)
				{ 
					var s = openFile.ReadLine();
			%>
			<option value="<%=s%>"><%=s%></option>
			<% 	
				}
				openFile.Close();
			%>
			>
		</select>
		<br />
		<input type="submit" value="Отправить" />
	</form>
	<% 
		}
		else 
		{ 
			openFile=file.OpenTextFile(abs_path.replace(/\\\w*\.asp/,"\\") + "response.txt", ForAdding);
			openFile.WriteLine(new Date() + " user " + strName + " ordered " + strChoise);
			openFile.close();
	%>
	<font><% =strName %>, спасибо за заказ.</font>
	<br />
	<font><% =strChoise %> будет доставлен до <% =	  date %>.</font>
	<%		} %>
</body>
</html>
