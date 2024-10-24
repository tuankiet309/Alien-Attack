public class BTTask_Weakened : BT_Node
{
    private BaseBossBehavior boss; // Tham chiếu đến boss
    
    public BTTask_Weakened(BaseBossBehavior boss)
    {
        this.boss = boss;
    }

    protected override NodeResult Execute()
    {
        // Kiểm tra xem boss có thực sự nên vào trạng thái suy yếu không
        if (!boss.IsArmorBroken())
        {
            return NodeResult.Failure; // Nếu không cần suy yếu, trả về thất bại
        }

        boss.EnterWeakenedState(); // Gọi phương thức để vào trạng thái suy yếu
        return NodeResult.Inprogress; // Trở về trạng thái đang diễn ra
    }

    protected override NodeResult Update()
    {
        if (boss.IsWeakenComplete()) // Kiểm tra xem trạng thái suy yếu đã hoàn thành chưa
        {
            return NodeResult.Success; // Nếu hoàn thành, trả về thành công
        }
        return NodeResult.Inprogress; // Nếu vẫn trong trạng thái suy yếu, trở về trạng thái đang diễn ra
    }
}