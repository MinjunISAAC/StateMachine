# StateMachine

**[StateMachine Repository]에는 2가지의 [StateMachine]이 구현되어있습니다.**


## StateMachine Version 1

### 설계 목적

- 캐릭터(Unit)의 움직임과 같이 볼륨이 적은 [State 관리]를 목적으로 설계

### 설계 구조


- **변수(Variables)**
  
  - 캐릭터(Unit)에서 필요한 상태(State)들을 열거형(Enum)으로 정의
  - 상태(State)를 저장할 [unitState] 변수 정의
  - 상태(State)에 따른 기능을 제공할 코루틴(Corutine) 정의

- **속성(Properties)**

  - Unit Class 밖에서 접근할 수 있는 UnitState 정의

- **함수(Functions)**





3. StateMachine version 1의 경우
