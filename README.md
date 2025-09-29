<h1 align="center"> 
🔴 NRT: Navigate, Transact, Retrieve 🔴
</h1>                                                    
           
---

<h3 align="center">
Developers: Nazim, Toufeeq, Ridhaa
</h3>

<p align="center">
<b>redAcademy</b> has a <b>Vending Machine</b> for Software Developer Sprinters to use, offering the perfect snacks for developers to handle bugs effectively.  
The <b>Admin</b> of the system must first add sprinters so they are able to login and use the NRT vending machine.  
</p>

---

---

## ⚙️ Steps to Run the Program

 **Configure database connection**  
- **Step 1:** Open `App.config`  
- **Step 2:** Update the `{SERVERNAME}` section with your **SQL Server name** from **SSMS**  

```xml
<connectionStrings>
  <add name="NRTDb" connectionString="Data Source={SERVERNAME};Initial Catalog=NRTVending;Integrated Security=True;" providerName="System.Data.SqlClient" />
</connectionStrings>
```

- **Step 3:** Go into Database.cs, and replace the private readonly string with yoru connection, example below:

---

<img width="1164" height="31" alt="image" src="https://github.com/user-attachments/assets/eea13640-d000-4d7c-a92a-b4db219d8678" />

---

- Then you want to copy everything from the SeedData.txt file into a new SQL query. Execute it, and... you're ready to test!

⚠️ Requirements
Visual Studio 2019 or later
.NET Framework 4.7.2
SQL Server (Express or Standard)
