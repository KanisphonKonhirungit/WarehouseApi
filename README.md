# Warehouse API

Warehouse API เป็นระบบสำหรับจัดการสินค้าในคลังสินค้า โดยใช้ .NET Core และ Entity Framework Core เพื่อจัดการข้อมูลในฐานข้อมูล SQL Server

## โครงสร้างโปรเจค

### Controllers
- **ProductsController.cs**: ควบคุมการทำงานเกี่ยวกับสินค้า (เพิ่ม, ลบ, แก้ไข, แสดงรายการ)

### Data
- **WarehouseContext.cs**: กำหนด `DbContext` สำหรับฐานข้อมูลสินค้า

### Models
- **ApplicationDbContext.cs**: กำหนด `DbContext` หลักของแอป
- **Product.cs**: โครงสร้างข้อมูลสินค้า (ID, Name, Quantity, Price, DateAdded)

### Configuration
- **appsettings.json**: กำหนดค่า `ConnectionStrings` สำหรับฐานข้อมูล SQL Server

### Startup & Middleware
- **Program.cs**: กำหนดการตั้งค่า API, Database, CORS และ Swagger UI

## การติดตั้งและการตั้งค่า

### ขั้นตอนที่ 1: Clone โปรเจค

```bash
git clone git@github.com:KanisphonKonhirungit/WarehouseApi.git
cd WarehouseApi
```

### ขั้นตอนที่ 2: ตั้งค่าฐานข้อมูล
สร้างฐานข้อมูล SQL Server ที่ชื่อว่า WarehouseDb
อัปเดต ConnectionStrings ในไฟล์ appsettings.json ให้ตรงกับข้อมูลการเชื่อมต่อฐานข้อมูลของคุณ
```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=WarehouseDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### ขั้นตอนที่ 3: รันโปรเจค
```
dotnet run
```
API จะทำงานที่ `http://localhost:5031`


## Restore ฐานข้อมูล (DB) ใน SQL Server
สามารถใช้ Backup DB WarehourseDB.bak ที่เป็นไฟล์ Backup ของฐานข้อมูล
นี่คือขั้นตอนในการ Restore ฐานข้อมูลใน SQL Server:
### 1. ผ่าน SQL Server Management Studio (SSMS)
- **ขั้นตอนที่ 1: เชื่อมต่อกับ SQL Serve**<br/>
เปิด SQL Server Management Studio (SSMS) และเชื่อมต่อกับ SQL Server instance ที่คุณต้องการจะ Restore ฐานข้อมูล

- **ขั้นตอนที่ 2: เลือก Database ที่จะ Restore**<br/>
คลิกขวาที่ Databases ใน Object Explorer และเลือก Restore Database...

- **ขั้นตอนที่ 3: เลือก Backup File**<br/>
ในหน้าต่าง Restore Database, เลือก Device ในส่วนของ Source และคลิกปุ่ม ... (Ellipsis)
คลิก Add และเลือกไฟล์ .bak ที่คุณต้องการจะ Restore จากตำแหน่งที่เก็บไฟล์นั้น

- **ขั้นตอนที่ 4: เลือก Options**<br/>
ในส่วน Destination, ตั้งชื่อฐานข้อมูลใหม่ หรือเลือกชื่อฐานข้อมูลเดิมที่ต้องการจะ Restore
ไปที่ Options ในเมนูด้านซ้าย เพื่อเลือกตัวเลือกเพิ่มเติม เช่น การ Overwrite หรือการเปลี่ยนแปลงไฟล์ฐานข้อมูลที่มีอยู่

- **ขั้นตอนที่ 5: เริ่มการ Restore**<br/>
คลิก OK เพื่อเริ่มการ Restore ฐานข้อมูล
รอจนกระทั่งขั้นตอนการ Restore เสร็จสมบูรณ์

### 2. ผ่านคำสั่ง T-SQL
- **ขั้นตอนที่ 1: ใช้คำสั่ง RESTORE DATABASE**<br/>
เปิด SQL Server Management Studio (SSMS) และเชื่อมต่อกับ SQL Server instance ที่คุณต้องการจะ Restore
เปิด New Query และใช้คำสั่ง SQL ต่อไปนี้:
```
RESTORE DATABASE WarehourseDB
FROM DISK = 'C:\path\to\your\WarehourseDB.bak'
WITH REPLACE;
```

- **ขั้นตอนที่ 2: ตรวจสอบการ Restore**<br/>
เมื่อคำสั่งสำเร็จ จะมีข้อความแจ้งเตือนว่า Restore เสร็จสมบูรณ์
คุณสามารถตรวจสอบฐานข้อมูลที่ถูก Restore โดยการดูที่ Databases ใน Object Explorer ของ SSMS

## การสนับสนุน
หากคุณพบปัญหาหรือมีคำถามเพิ่มเติมเกี่ยวกับโปรเจคนี้ กรุณาติดต่อที่ kanisphon.konh@outlook.com
