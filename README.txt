안녕하세요.

1.
이 게임?은 아직 완성은 하지 못했습니다.
게임의 의도는 애니풍의 주인공이 맵에서 통통 튀어다니고, 사실감있는 좀비와 싸우는 것을 구상했습니다.
싸움은 플레이어가 좀비를 밟거나 총으로 쏘아 잡는 것을 구상했는데 현재 밟는 것까지만 구현했습니다.
그리고 좀비에게 NavMesh를 적용하는 것까지도 목표했습니다만 시간이 부족해 아쉽습니다.

2.
현재 게임에 구현한 기능은 아래와 같습니다.

- 좀비:	
	-사망/드랍 로직: 머리에 콜리더를 두고 트리거되면 사방으로 아이템을 드랍하는 식으로 구현했습니다.

- 점프대(버섯 모양)
	-점프대를 밟으면 노말벡터 방향으로 플레이어를 튕겨내게 했습니다. 노말벡터는 충돌시 검출되는 ContactPoint[]를 통해 구했습니다.
	-플레이어는 기본적으로 바닥 판정(충돌 경사점의 각도가 40도 이하)이 되는 오브젝트와 충돌하면 탄력을 받습니다.
	-점프대는 플레이어가 기본으로 받는 탄력에 추가적인 힘을 더해줍니다.

- 플레이어(움직임, 점프(도약,부양,착지 모션 o), 걷기)
	-움직임은 입력값을 기반으로 움직이게 했습니다. rigidbody 쓰는 게 필수과제인 걸 늦게 봤는데 손대기엔 너무 늦어서 그냥 했습니다..ㅠ
	-점프는 rigidbody ForceMode를 사용했습니다. 에셋에 다양한 모션이 있어 최대한 살렸습니다. 
	-점프 로직에 쓰이는 isGround 판정은, 콜리더와의 충돌 단계마다 바닥인지 검사하여 touchingGrounds[]에 넣어주는 방식으로 했습니다. 
	-cf)관련 트러블 슈팅이 있었는데, 좀비를 머리를 밟을때 좀비 머리 콜라이더와 CollisionEnter하게 되어 바닥 판정을 갖게 됩니다. 그런데 이때 좀비가 죽어서 바로 사라져버리는 바람에 CollisionExit은 발동하지 않게되었습니다. CollisionExit은 touchingGrounds[]에서 collider를 제거해주는 로직이 있기 때문에,  이게 동작하지 않으면 계속해서 바닥으로 인식하게 됩니다.(무한 점프 가능) 해결하기 위해서 CollisionEnter시에 삭제된 collider를 별도로 제거해주는 로직을 추가했습니다. 근데 완벽한 해결법은 아닌 거 같습니다. 

- UI 
	-좌측 하단에 스태미나, 체력, 총알: 총알은 총을 구현하지 못해서 사용할 일이 없게 되었습니다.
	-인벤토리: 인벤토리 UI와 플레이어 인벤토리를 구분했습니다. 뷰와 모델의 구분을 해보려고 했는데 구현하고 사용하다보니 유니티에서는 그냥 하나로 합쳐서 관리하는 것도 나쁘지 않겠다는 생각이 들었습니다. 
	-아이템 위에 마우스를 가져가면 간단한 아이템 정보 팝업창을 띄우게 했습니다. 팝업창 위에 마우스가 hovering 중일때는 팝업창이 사라지지 않게 했습니다. 여기서 팝업창의 표시와 사라짐을 위해 코루틴을 사용했는데, 버그가 많이 생겼습니다. 아직도 많이 생길 것 같습니다.

아이템(좀비 파편, '노릇한 고기')
	- 아이템은 ScriptableObject를 상속한 ItemData로 원본 데이터를 가지게 했습니다. 그리고 ItemData를 감싸고 있는 클래스로 Drop_Item(필드에 존재)과 Inven_Item(인벤토리에 존재)로 나눴습니다. 
	-아이템 파밍은 IInteractable을 통해 구현했습니다.
	
- 모닥불
	-모닥불은 요리가 가능합니다. IInteractable을 통해 구현했습니다.
	-맛있는 고기 드시고 가세요.

3. 어려웠던 부분&피드백 원하는 부분
	- 현재 플레이어 움직임이 현재 점프, 달리기, 걷기,(뒤로 달리기, 뒤로 걷기) 정도가 있습니다. 원래는 이러한 움직임 상태를 enum으로 관리하려했는데 막상 구현하고 보니 enum이 들어갈 자리가 없었던 거 같습니다. 이래도 괜찮을지 아니면 개선할 부분이 있을지 궁금합니다.
	- 카메라 시점과 그에 따른 플레이어의 움직임을 더 개선하고 싶었습니다. 특히 A,D키를 누르면 옆으로 움직이는데 모션은 그냥 앞뒤로 움직이는 모션이 나와서 좀 아쉬웠습니다.
	- 아이템 팝업창 관련해서, IPointerHandler 인터페이스 활용하여서 Item Slot 위에 마우스를 올리면 아이템 팝업창이 나오고, 마우스가 슬롯이나 팝업창을 벗어나면 팝업창이 사라지게 구현했는데, 구현 로직이 조금 직관적이지 않은 거 같아 아쉽습니다.(특히, isHoverSlot, isHoverPopup 두가지 변수를 다루는 방식이 아쉽습니다) 혹시 더 좋은 방법이 있을까요?
	- 그 외에도 아쉬운 부분 말씀해주시면 열심히 새겨듣겠습니다