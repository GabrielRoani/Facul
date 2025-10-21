namespace acessa_dev_web.wwwroot.css
{
    public class crud_styles
    {
    }
}

body {
    background-color: #f4f7f6;
    font - family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
color: #333;
    margin: 0;
padding: 20px;
}

.crud - container {
    max - width: 800px;
margin: 40px auto;
padding: 30px;
    background - color: #ffffff;
    border - radius: 8px;
    box - shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
}

.crud - container h1 {
    color: #2c3e50;
    margin - bottom: 25px;
text - align: center;
border - bottom: 2px solid #ecf0f1;
    padding-bottom: 15px;
}

.styled - table {
width: 100 %;
    border - collapse: collapse;
    margin - top: 20px;
}

.styled - table th, .styled-table td {
    padding: 12px 15px;
border - bottom: 1px solid #ddd;
    text-align: left;
}

.styled - table th {
    background-color: #3498db;
    color: #ffffff;
    font - weight: bold;
}

.styled - table tbody tr:hover {
    background-color: #f5f5f5;
}

.form - group, .details - group {
    margin - bottom: 20px;
}

.form - label, .details - label {
display: block;
    font - weight: bold;
    margin - bottom: 5px;
color: #555;
}

.form - control {
width: 100 %;
padding: 10px;
border: 1px solid #ccc;
    border - radius: 4px;
    box - sizing: border - box; 
}

.details - value {
padding: 10px;
    background - color: #ecf0f1;
    border - radius: 4px;
}


.action - buttons {
    margin - top: 30px;
display: flex;
    justify - content: flex - end;
gap: 10px;
}

.btn {
    padding: 10px 20px;
border: none;
border - radius: 5px;
color: white;
text - decoration: none;
cursor: pointer;
font - size: 16px;
text - align: center;
}

.btn - success {
    background - color: #27ae60; }
.btn - warning {
        background - color: #f39c12; }
.btn - danger {
            background - color: #e74c3c; }
.btn - info {
                background - color: #3498db; }
.btn - secondary { background - color: #7f8c8d; color: white; }