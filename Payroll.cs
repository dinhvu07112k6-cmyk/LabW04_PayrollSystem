/*********************************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
Lớp: 20261-174373-LT HDT
*********************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabW04_PayrollSystem;

public class Payroll
{
    public string Period { get; }
    private readonly List<Employee> employees = [];

    public Payroll(string period)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(period, nameof(period));
        Period = period;
    }

    public bool AddEmployee(Employee employee)
    {
        if (employee is null) return false;

        // Bất biến: Không trùng lặp mã nhân sự
        if (employees.Any(e => string.Equals(e.EmployeeId, employee.EmployeeId, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"-> Lỗi: Đã tồn tại mã nhân sự {employee.EmployeeId} trong bảng lương!");
            return false;
        }

        employees.Add(employee);
        return true;
    }

    public Employee? FindEmployee(string employeeId)
    {
        return employees.FirstOrDefault(e => string.Equals(e.EmployeeId, employeeId, StringComparison.OrdinalIgnoreCase));
    }

    public double CalculateTotalPayroll()
    {
        return employees.Sum(e => e.CalculateGrossPay());
    }

    public double CalculatePayrollByDepartment(string department)
    {
        return employees
            .Where(e => string.Equals(e.Department, department, StringComparison.OrdinalIgnoreCase))
            .Sum(e => e.CalculateGrossPay());
    }

    public Employee? FindHighestPaidEmployee()
    {
        if (employees.Count == 0) return null;
        return employees.OrderByDescending(e => e.CalculateGrossPay()).FirstOrDefault();
    }

    public void DisplayPayroll()
    {
        Console.WriteLine($"\n=========================================================================================");
        Console.WriteLine($"                                BẢNG LƯƠNG KỲ: {Period}");
        Console.WriteLine($"=========================================================================================");

        if (employees.Count == 0)
        {
            Console.WriteLine("Danh sách nhân sự hiện đang trống!");
            Console.WriteLine("=========================================================================================\n");
            return;
        }

        foreach (var emp in employees)
        {
            emp.DisplayPayrollInfo(); // Đa hình thời điểm chạy
        }

        Console.WriteLine("-----------------------------------------------------------------------------------------");
        Console.WriteLine($"-> Tổng chi phí lương toàn doanh nghiệp: {CalculateTotalPayroll():N0} VND");
        Console.WriteLine($"-> Tổng quỹ lương phòng 'Hỗ trợ':        {CalculatePayrollByDepartment("Hỗ trợ"):N0} VND");

        var highest = FindHighestPaidEmployee();
        if (highest is not null)
        {
            Console.WriteLine($"-> Người có thu nhập cao nhất:           {highest.FullName} ({highest.EmployeeId}) - {highest.CalculateGrossPay():N0} VND");
        }
        Console.WriteLine($"=========================================================================================\n");
    }
}