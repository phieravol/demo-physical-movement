# PRU211m - Slot5 : Demo addForce & Move GameObject by Force

## 1. Environment

 - Unity
 - Asp.net6
 - Visual studio 2022

## 2. Step by Step

 1.  Get gameobject & Rigidbody2d 
 2. 

### 2.1. Get GameObject

  **[#1 [GameObject](https://docs.unity3d.com/ScriptReference/GameObject.html).Find("objectName") ]** 
  

 - Hữu ích khi tự động kết nối với các game object khác tại thời điểm tải, bên trong [MonoBehaviour.Awake](https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html) hoặc [MonoBehaviour.Start](https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html).
 - VD:

        private GameObject hand;  
      
        void Start()
        {
            hand = GameObject.Find("/Monster/Arm/Hand");
        }  
      
        void Update()
        {
            hand.transform.Rotate(0, 100 * Time.deltaTime, 0);
        }

 
 
### 2.2. Handle logic in FixedUpdate()

 - Trong khi Update() chỉ thực thi 1 lần/khung hình và **độc lập với Physical Engine** thì FixedUpdate() có thể thực thi một hoặc nhiều lần / khung hình **đồng bộ với Physical Engine**
  ⇒ khi xử lý tác dụng lực bạn cần code trong FixedUpdate() method.

**[#1.  Get Input keyboard of user]**

 - Có nhiều cách để nhận cần điều khiển (joystick) của người dùng, ở đây ta dùng [Input.GetAxis](https://docs.unity3d.com/ScriptReference/Input.GetAxis.html)("axisName")

            // Get the horizontal and vertical axis.
            // By default they are mapped to the arrow keys.
            // The value is in the range -1 to 1
            InputHorizontal = Input.GetAxis("Horizontal");
            InputVertical = Input.GetAxis("Vertical");
   
 - Giá trị trả về nằm trong khoảng từ [-1, 1]
 - Hàm này có thể sử dụng để nhận biết hướng chuyển động của gameobject theo hệ trục tọa độ. 
 - Ví dụ  
	   - Input.GetAxis("Horizontal") > 0 ==> gameobject di chuyển **cùng chiều** với trục ngang
	   - Input.GetAxis("Horizontal") < 0 ==>gameobject di chuyển **ngược chiều** với trục ngang
 - Chú ý: Cần phân biệt [Input.GetAxis](https://docs.unity3d.com/ScriptReference/Input.GetAxis.html) với [Input.GetAxisRaw](https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html) để sử dụng trong từng trường hợp. Trong khi giá trị trả về của `Input.GetAxis` có thể tăng dần từ 0 đến 1 hoặc giảm dần từ 0 đến -1 thì `Input.GetAxisRaw` chỉ nhận một trong ba giá trị {-1, 0, 1}

**[#2.  Add Force for player]**

 - Sử dụng **`Rigidbody.AddForce(vector, ForceMode)`**
 - Lưu ý về [ForceMode](https://docs.unity3d.com/ScriptReference/ForceMode.html): Trong Unity 2d có hai force mode
	 - ForceMode.Force: diễn giải đầu vào là đơn vị Newton, vật bị tác động thay đổi vận tốc theo thời gian `(Force/m).deltaTime`
	 - ForceMode.Impulse: diễn giải đầu vào là xung lực  p = ![Xung lượng của lực là gì Đơn vị của xung lượng của lực là gì](https://vietjack.com/tai-lieu-mon-ly/images/xung-luong-cua-luc-la-gi-don-vi-cua-xung-luong-cua-luc-la-gi-33314.png) = m.v ,  khi sử dụng ForceMode này thì vận tốc được thay đổi tức thì trong từng khung hình v = p/m
 - Ví dụ:
```
void FixedUpdate()
{
	Vector2 ForceVector = new Vector2(10f, 7f);
	rb2D.AddForce(ForceVector);
	// ForceMode2D.Force is default ForceMode
	rb2D.AddForce(ForceVector, ForceMode2D.Impulse);
}
```


