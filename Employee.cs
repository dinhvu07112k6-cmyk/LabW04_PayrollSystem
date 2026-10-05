/*********************************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
Lớp: 20261-174373-LT HDT
*********************************/
using System;
using System.Collections.Generic;

namespace LabW04_PayrollSystem;

public abstract class Employee
{
    public string EmployeeId { get; }
    public string FullName { get; }
    public string Department { get; }
    public double MonthlyBonus { get; protected set; }

    // Lưu lịch sử thưởng bằng danh sách đối tượng BonusRecord
    protected readonly List<BonusRecord> bonusHistory = [];

    // Constructor rút gọn
    public Employee(string employeeId, string fullName)
        : this(employeeId, fullName, "Unassigned") { }

    // Constructor đầy đủ
    public Employee(string employeeId, string fullName, string department)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(employeeId, nameof(employeeId));
        ArgumentException.ThrowIfNullOrWhiteSpace(fullName, nameof(fullName));
        ArgumentException.ThrowIfNullOrWhiteSpace(department, nameof(department));

        EmployeeId = employeeId;
        FullName = fullName;
        Department = department;
        MonthlyBonus = 0;
    }

    // Nạp chồng 1: Thưởng tiền mặt cố định không lý do
    public void AddBonus(double amount)
    {
        AddBonus(amount, "Thưởng không kèm lý do");
    }

    // Nạp chồng 2: Thưởng tiền mặt cố định có lý do
    public void AddBonus(double amount, string reason)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Khoản thưởng phải lớn hơn 0!");
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Lý do thưởng không được để trống!", nameof(reason));

        MonthlyBonus += amount;
        // Ghi lại lịch sử thưởng vào danh sách
        bonusHistory.Add(new BonusRecord(amount, reason));
    }

    // Nạp chồng 3: Thưởng theo tỷ lệ phần trăm của mốc tham chiếu có lý do
    public void AddBonus(double rate, double referenceAmount, string reason)
    {
        if (rate <= 0 || rate > 0.5)
            throw new ArgumentOutOfRangeException(nameof(rate), "Tỷ lệ thưởng phải lớn hơn 0 và không quá 0.5 (50%)!");
        if (referenceAmount <= 0)
            throw new ArgumentOutOfRangeException(nameof(referenceAmount), "Giá trị tham chiếu phải lớn hơn 0!");
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Lý do thưởng không được để trống!", nameof(reason));

        double bonusCalculated = referenceAmount * rate;
        MonthlyBonus += bonusCalculated;
        // Ghi lại lịch sử thưởng vào danh sách
        bonusHistory.Add(new BonusRecord(bonusCalculated, $"{reason} ({rate * 100}% của {referenceAmount:N0} VND)"));
    }

    // Đặt lại thưởng khi bắt đầu kỳ lương mới
    public void ResetBonus()
    {
        MonthlyBonus = 0;
        bonusHistory.Clear();
    }

    // Trả về danh sách lịch sử thưởng
    public IReadOnlyList<BonusRecord> GetBonusHistory() => bonusHistory.AsReadOnly();

    // Phương thức thuần ảo bắt buộc các lớp con ghi đè
    public abstract double CalculateGrossPay();
    public abstract string GetEmployeeType();

    public virtual void DisplayPayrollInfo()
    {
        Console.WriteLine($"Mã: {EmployeeId,-6} | Họ tên: {FullName,-18} | Phòng: {Department,-12} | Chức danh: {GetEmployeeType(),-18} | Thưởng: {MonthlyBonus,11:N0} | Thu nhập: {CalculateGrossPay(),12:N0} VND");
    }
}