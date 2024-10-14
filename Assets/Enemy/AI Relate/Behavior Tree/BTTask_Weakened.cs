public class BTTask_Weakened : BT_Node
{
    BaseBossBehavior boss; // Tham chiếu đến boss
    
    public BTTask_Weakened(BaseBossBehavior boss)
    {
        this.boss         = boss;
    }

    protected override NodeResult Execute()
    {
        boss.EnterWeakenedState(); // Gọi phương thức để vào trạng thái suy yếu
        return NodeResult.Inprogress; // Trở về trạng thái đang diễn ra
    }

    protected override NodeResult Update()
    {
        if (boss.IsWeakenedComplete()) // Kiểm tra xem trạng thái suy yếu đã hoàn thành chưa
        {
            return NodeResult.Success; // Nếu hoàn thành, trả về thành công
        }
        return NodeResult.Inprogress; // Nếu vẫn trong trạng thái suy yếu, trở về trạng thái đang diễn ra
    }
}