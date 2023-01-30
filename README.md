# PRU211m  Slot5 : AddForce & Move GameObject By Force

## 1. Environment

 - Unity
 - Asp.net6
 - Visual studio 2022

## 2. Code Flow

2.1 . Get gameobject & Rigidbody2d 
2.2. Handle Game Logic in FixedUpdate()
- Get Input keyboard
- Add Force for player
 
2.3. Handle Collision & Other Logic (Optional)
- Players can only jump when they are Ground
- Prevent players from falling when they collide with the ball
- Pull ball to player when click mouse


### 2.1. Get GameObject

  **#1. [GameObject](https://docs.unity3d.com/ScriptReference/GameObject.html).Find("objectName")**
  

 - Trả về GameObject với tham số là name của GameObject đó, có thể chỉ định thêm GameObject cha để lấy đúng gameobject mà bạn cần <3
 - VD:

        private GameObject hand;  
      
        void Start()
        {
	        // #1 - Get GameObject "Hand" by name (Normal)
	        hand = GameObject.Find("Hand");
	        
	        // #2 - Get GameObject "Hand" of Monster (Parent GameObject)
            hand = GameObject.Find("/Monster/Arm/Hand");
			
			// #3 - Get GameObject "Hand" with no Parent
			hand = GameObject.Find("/Hand");
        }  
      
  **#2 [GameObject](https://docs.unity3d.com/ScriptReference/GameObject.html).GetComponent<<Rigidbody2D>Rigidbody2D>()** 
  
  - Khi thao tác với lực hay bất kỳ thành phần nào khác trong engine vật lý trong unity, bạn cần lấy đúng Rigidbody2d của GameObject cần xử lý.
 
 - VD: 
 ```
 private GameObject gameObject1;
 void Start()
 {
        gameObject1 = GameObject.Find("GameObjectName");
        RigidGameObj1 = gameObject1.GetComponent<Rigidbody2D>();
}
 ```
### 2.2. Handle logic in FixedUpdate()

 - Trong khi Update() chỉ thực thi 1 lần/khung hình và **độc lập với Physical Engine** thì FixedUpdate() có thể thực thi một hoặc nhiều lần / khung hình **đồng bộ với Physical Engine**
  ⇒ khi xử lý tác dụng lực bạn cần code trong FixedUpdate() method.

**#1.  Get Input keyboard of user**

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

**#2.  Add Force for player**

 - Sử dụng **`Rigidbody.AddForce(vector, ForceMode)`**
 - Lưu ý về [ForceMode](https://docs.unity3d.com/ScriptReference/ForceMode.html): Trong Unity 2d có hai force mode
	 - ForceMode.Force: diễn giải đầu vào là đơn vị Newton, vật bị tác động thay đổi vận tốc theo thời gian `(Force/m).deltaTime`
	 - ForceMode.Impulse: diễn giải đầu vào là xung lực  p = ![Xung lượng của lực là gì Đơn vị của xung lượng của lực là gì](https://vietjack.com/tai-lieu-mon-ly/images/xung-luong-cua-luc-la-gi-don-vi-cua-xung-luong-cua-luc-la-gi-33314.png) = m.v ,  khi sử dụng ForceMode này thì vận tốc được thay đổi tức thì trong từng khung hình v = p/m
	  
 - Ví dụ khi AddForce với các ForceMode khác nhau:
```
void FixedUpdate()
{
	//Declare force vector
	Vector2 ForceVector = new Vector2(10f, 7f);

	// AddForce with force mode is ForceMode2D.Force
	rb2D.AddForce(ForceVector, ForceMode2D.Force);
	
	// AddForce with force mode is ForceMode2D.Impulse
	rb2D.AddForce(ForceVector, ForceMode2D.Impulse);
}
```
### 2.3. Handle Collision & Other Logic (Optional)
- Tất cả kiến thức căn bản cần có đã được trình bày ở trên, đây là phần demo thêm để củng cố game logic.

## 3. Installation guideline

 1. Clone repository into local
 2. Unity -> Open -> add project from disk -> select **demo-physical-movement** folder
 3. **Assets** -> **Scenes** -> double click on **SampleScenes**
