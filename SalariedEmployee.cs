/*********************************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
Lớp: 20261-174373-LT HDT
*********************************/
using System;

namespace LabW04_PayrollSystem;

public class SalariedEmployee : Employee
{
    public double MonthlySalary { get; }
    public double ResponsibilityAllowance { get; }

    // Constructor rút gọn (không có phụ cấp)
    public SalariedEmployee(string employeeId, string fullName, string department, double monthlySalary)
        : this(employeeId, fullName, department, monthlySalary, 0) { }

    // Constructor đầy đủ
    public SalariedEmployee(string employeeId, string fullName, string department, double monthlySalary, double responsibilityAllowance)
        : base(employeeId, fullName, department)
    {
        if (monthlySalary < 0)
            throw new ArgumentOutOfRangeException(nameof(monthlySalary), "Lương tháng không được âm!");
        if (responsibilityAllowance < 0)
            throw new ArgumentOutOfRangeException(nameof(responsibilityAllowance), "Phụ cấp trách nhiệm không được âm!");

        MonthlySalary = monthlySalary;
        ResponsibilityAllowance = responsibilityAllowance;
    }

    public override double CalculateGrossPay()
    {
        return MonthlySalary + ResponsibilityAllowance + MonthlyBonus;
    }

    public override string GetEmployeeType() => "Lương cố định";

    public override void DisplayPayrollInfo()
    {
        Console.WriteLine($"Mã: {EmployeeId,-6} | Họ tên: {FullName,-18} | Phòng: {Department,-12} | Lương tháng: {MonthlySalary,10:N0} | Phụ cấp: {ResponsibilityAllowance,9:N0} | Thưởng: {MonthlyBonus,9:N0} | Thu nhập: {CalculateGrossPay(),11:N0} VND");
    }
}