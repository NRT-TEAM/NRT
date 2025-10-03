<!-- Header NRT  -->
<h1 align="center"> 
🔴 NRT: Navigate, Transact, Retrieve 🔴
</h1>                                                     

---
<!-- Images of NRT LOGO and Vending Vector Art  -->
<h1 align="center"> 
<img width="365" height="200" alt="image" src="https://github.com/user-attachments/assets/4687a074-0437-4fd5-8ce4-7869bb5f90ea"/>
<img width="200" height="200" alt="image" src="https://github.com/user-attachments/assets/5198a254-8698-4ab2-846d-846ec44cef64" />
</h1> 

<p><br> <br> </p>
<!-- Description -->
<p align="center"> 
<b> C# Console Application simulating a Vending Machine.</b>
</p>

<p align="center"> 
<b> 💻 PHASE 1:</b> Initial prototype with <b>hardcoded data</b> (no database). ✔<br> 
<b> 💻 PHASE 2:</b> Implemented <b>OOP, ADO.NET, and SMMS</b> for CRUD functionality. ✔ 
</p>

<p><br> <br> </p>



---
<!-- Header Scenario  -->
<h1 align="center">
🍀 <b>Scenario:</b> 🍀
</h1>

---

<!-- redAcademy Images -->
<p align="center"> 
<img width="200" height="200" alt="image" src="https://github.com/user-attachments/assets/d2380e2e-be28-40f0-98a1-6cde552e2622" />
<img width="200" height="200" alt="image" src="https://github.com/user-attachments/assets/e9ac5e7f-f0a1-4e0b-b49d-078308c23702" />
</p>

---
<!-- Description -->
<p align="center">
<b>redAcademy</b> has a <b>Vending Machine</b> for Software Developer Sprinters to use, offering the perfect snacks for developers to handle bugs effectively.  
The <b>Admin</b> of the system must <b>first add sprinters</b> so they are able to login and use the NRT vending machine.  
</p>

---
<p align="center">
<img width="965" height="596" alt="image" src="https://github.com/user-attachments/assets/c73b6205-5dfd-427c-829f-17935be2110d" />
</p> 
<p align="center">
  <img width="712" height="473" alt="image" src="https://github.com/user-attachments/assets/abbfa9e6-b7b6-4b25-868b-9532ac4a0c0b" />
</p>

<p><br> <br> </p>
<p><br> <br> </p>

---

<h1 align="center">  ⚙️ Steps to Run the Program ⚙️ </h1>

---

- **Step 1:** Open `App.config`  
- **Step 2:** Update the `{SERVERNAME}` section with your **SQL Server name** from **SSMS** 

```xml
<connectionStrings>
  <add name="NRTDb" connectionString="Data Source={SERVERNAME};Initial Catalog=NRTVending;Integrated Security=True;" providerName="System.Data.SqlClient" />
</connectionStrings>
```

- **Step 3:** Go into Database.cs, and replace the private readonly string with yoru connection, example below:

<img width="1164" height="31" alt="image" src="https://github.com/user-attachments/assets/eea13640-d000-4d7c-a92a-b4db219d8678" />

- **Step 3:** Then you want to copy everything from the **SeedData.txt** file into a new SQL query. Execute it, and... **you're ready to test!**
  
---

<p><br> <br> </p>
<p><br> <br> </p>


---
<!-- Header Scenario  -->
<h1 align="center">
💻 <b>System Overview:</b> 💻 
</h1>

---

### ⚠️ Requirements
- Visual Studio 2019 or later
- .NET Framework 4.7.2
- SQL Server (Express or Standard)
  
<p><br></p>


### 🛠️ Technologies & Libraries
- C# (.NET Framework 4.7.2)
- ADO.NET (Database access)
- SQL Server (SMSS for DB management)
- Spectre.Console (Rich console UI)
- System.Media (Audio feedback)
- OOP Principles

<p><br></p>

### ✨ Features
- Admin can **Create, Read, Update, Delete** (CRUD) sprinters in the database  
- Sprinters can log in and interact with the vending machine  
- Seed data provided for quick setup  
- Console UI with Spectre.Console  
- Audio feedback using System.Media

<p><br></p>

---



