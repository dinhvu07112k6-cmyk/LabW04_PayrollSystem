/*********************************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
Lớp: 20261-174373-LT HDT
*********************************/
using System;

namespace LabW04_PayrollSystem;

public class SalesEmployee : Employee
{
    public double BaseSalary { get; }
    public double SalesRevenue { get; private set; }
    public double CommissionRate { get; }

    // Constructor rút gọn (doanh số ban đầu = 0)
    public SalesEmployee(string employeeId, string fullName, string department, double baseSalary, double commissionRate)
        : this(employeeId, fullName, department, baseSalary, 0, commissionRate) { }

    // Constructor đầy đủ
    public SalesEmployee(string employeeId, string fullName, string department, double baseSalary, double salesRevenue, double commissionRate)
        : base(employeeId, fullName, department)
    {
        if (baseSalary < 0)
            throw new ArgumentOutOfRangeException(nameof(baseSalary), "Lương cơ bản không được âm!");
        if (salesRevenue < 0)
            throw new ArgumentOutOfRangeException(nameof(salesRevenue), "Doanh số không được âm!");
        if (commissionRate < 0 || commissionRate > 0.3)
            throw new ArgumentOutOfRangeException(nameof(commissionRate), "Tỷ lệ hoa hồng phải từ 0 đến 0.3 (30%)!");

        BaseSalary = baseSalary;
        SalesRevenue = salesRevenue;
        CommissionRate = commissionRate;
    }

    public void UpdateSalesRevenue(double additionalRevenue)
    {
        if (additionalRevenue < 0)
            throw new ArgumentOutOfRangeException(nameof(additionalRevenue), "Doanh số cập nhật không được âm!");
        SalesRevenue += additionalRevenue;
    }

    public override double CalculateGrossPay()
    {
        return BaseSalary + (SalesRevenue * CommissionRate) + MonthlyBonus;
    }

    public override string GetEmployeeType() => "Kinh doanh";

    public override void DisplayPayrollInfo()
    {
        Console.WriteLine($"Mã: {EmployeeId,-6} | Họ tên: {FullName,-18} | Phòng: {Department,-12} | Doanh số: {SalesRevenue,11:N0} | Hoa hồng: {CommissionRate * 100,2}% | Thưởng: {MonthlyBonus,9:N0} | Thu nhập: {CalculateGrossPay(),11:N0} VND");
    }
}