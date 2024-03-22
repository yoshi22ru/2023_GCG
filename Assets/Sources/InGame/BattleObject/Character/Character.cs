using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using Sources.InGame.BattleObject.Skill;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.InGame.BattleObject.Character
{

    public class Character : BattleObject
    {
        private int _currentBuff;
        private MoveClass _moveClass;
        private Vector3 _moveVector;
        public Animator animator;
        private CancellationTokenSource _toIdleToken;
        private CancellationToken _token;

        protected CharacterState CurrentState
        {
            private set;
            get;
        }
        protected CharacterStatus CharacterStatus;
        private BuffManager _buffManager;
        [SerializeField] protected Transform myTransform;
        protected Vector3 p1, p2, v1, v2;
        private half _elapsedTime;
        private const float InterpolationPeriod = 0.25f;

        #region skill variables

        [SerializeField] GameObject skill1;
        [SerializeField] GameObject skill2;
        [SerializeField] GameObject special;
        [SerializeField] Transform skill1Point;
        [SerializeField] Transform skill2Point;
        [SerializeField] Transform specialPoint;
        

        #region Getter

        protected GameObject Skill1Prefab
        {
            get { return skill1; }
        }

        protected GameObject Skill2Prefab
        {
            get { return skill2; }
        }

        protected GameObject SpecialPrefab
        {
            get { return special; }
        }

        protected Transform Skill1Point
        {
            get { return skill1Point; }
        }

        protected Transform Skill2Point
        {
            get { return skill2Point; }
        }

        protected Transform SpecialPoint
        {
            get { return specialPoint; }
        }
        #endregion
        
        #endregion

        #region Animation Variables

        // Animation Clip Names
        private const string AIdle = "Idle";
        private const string ARun = "Run";
        private const string ADamage = "Damage";
        private const string ADead = "Dead";
        private const string ASkill1 = "Skill1";
        private const string ASkill2 = "Skill2";
        private const string ASpecial = "Special";

        private Dictionary<CharacterState, AnimationInfo> _animationInfos;

        #endregion

        protected enum CharacterState
        {
            Idle,
            Run,
            Damage,
            Dead,
            Skill1,
            Skill2,
            Special,
        }

        private void Awake()
        {
            CharacterStatus = GetComponent<CharacterStatus>();
            _toIdleToken = new CancellationTokenSource();
            InitAnim();
            var rbody = GetComponent<Rigidbody>();
            SetState(CharacterState.Idle);
            _moveClass = new MoveClass(myTransform, rbody, CharacterStatus);
        }

        private void Start()
        {
            SetTeam(VariableManager.GetTeamByActorNumber(photonView.OwnerActorNr));
            Debug.Log("Called Init BattleObject\n" +
                      $"{Utils.FormatBattleObjectInformation(gameObject, this)}");
            if (photonView.IsMine)
            {
                Debug.Log(this.name + " : " + photonView.IsMine);
                CameraManager.instance.myCharacter = this.gameObject;
                CameraManager.instance.Initialize();
            }
        }

        protected override void OnHitMyTeamObject(BattleObject battleObject)
        {
            SkillManager skillManager = battleObject as SkillManager;
            if (skillManager == null) return;

            if (skillManager.type == SkillManager.SkillType.Heal)
            {
                CharacterStatus.SetHP(skillManager.GetHeal + CharacterStatus.CurrentHP);
            }
            else if (skillManager.type == SkillManager.SkillType.BufSpeed)
            {
                CharacterStatus.SetMoveSpeed(skillManager.GetBufSpeed + CharacterStatus.MoveSpeed);
            }
            else if (skillManager.type == SkillManager.SkillType.BufAttack)
            {
                GameObject[] skill = CharacterStatus.GetSkillPrefab;
                for (int i = 0; i < skill.Length; i++)
                {
                    skillManager = skill[i].gameObject.GetComponent<SkillManager>();
                    skillManager.SetSkillDamage(skillManager.GetSkillDamage + 15);
                }
            }
        }

        protected override void OnHitEnemyTeamObject(BattleObject battleObject)
        {
            var skillManager = battleObject as SkillManager;
            if (skillManager == null) return;

            if (skillManager.type == SkillManager.SkillType.Damage)
            {
                Damage(skillManager);
            }
        }

        protected void Damage(SkillManager skillManager)
        {
            Debug.Log("Damage Hit\n" +
                      Utils.FormatBattleObjectInformation(gameObject, this) +
                      $"Value : {skillManager.GetSkillDamage}\n");
            CharacterStatus.SetHP(CharacterStatus.CurrentHP - skillManager.GetSkillDamage);
            SetStateAndResetIdle(CharacterState.Damage);
        }

        private async void OnDead()
        {
            gameObject.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(GameManager.RespawnTime));
            
            GameManager.manager.ReSetPosition(this);
            gameObject.SetActive(true);
        }


        // private void FixedUpdate()
        // {
        //     if (photonView.IsMine)
        //     {
        //         p1 = p2;
        //         p2 = myTransform.position;
        //         _elapsedTime = (half)Time.deltaTime;
        //     }
        //     else
        //     {
        //         _elapsedTime += (half)Time.deltaTime;
        //         if (_elapsedTime < InterpolationPeriod)
        //         {
        //             myTransform.position =
        //                 HermiteSpline.Interpolate(p1, p2, v1, v2, _elapsedTime / InterpolationPeriod);
        //         }
        //         else
        //         {
        //             myTransform.position = Vector3.LerpUnclamped(p1, p2, _elapsedTime / InterpolationPeriod);
        //         }
        //
        //         return;
        //     }
        //
        //     if (CharacterStatus.IsDead)
        //     {
        //         if (CurrentState != CharacterState.Dead)
        //         {
        //             SetState(CharacterState.Dead);
        //         }
        //
        //         CharacterStatus.SetHP(CharacterStatus.MaxHP);
        //     }
        //
        //     if (CharacterStatus.IsDead == false && CountDown.instance.isCountFinish == true)
        //     {
        //         // FIXME
        //         // else if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.K))
        //         // {
        //         //     characterStatus.SetIsDead(true);
        //         // }
        //     }
        // }

        protected void SetState(CharacterState newState)
        {
            animator.ResetTrigger(_animationInfos[CurrentState].Id);

            CurrentState = newState;

            animator.SetTrigger(_animationInfos[newState].Id);
        }

        protected async void SetStateAndResetIdle(CharacterState state)
        {
            SetState(state);
            if (_token.CanBeCanceled)
            {
                _toIdleToken.Cancel();
                _toIdleToken = new CancellationTokenSource();
            }

            _token = _toIdleToken.Token;
            await UniTask.Delay(TimeSpan.FromSeconds(_animationInfos[state].Length), cancellationToken: _token).SuppressCancellationThrow();
            SetState(CharacterState.Idle);
        }

        protected virtual void Skill1()
        {
        }

        protected virtual void Skill2()
        {
        }

        protected virtual void Special()
        {
        }

        // FIXME
        // void IPunObservable.OnPhotonSerializeView(Photon.Pun.PhotonStream stream, Photon.Pun.PhotonMessageInfo info) {
        //     if (stream.IsWriting) {
        //         // send
        //         short vx,vy,vz;
        //         (vx, vy, vz) = Protcol.PositionSerialize(my_transform.position);
        //         stream.SendNext(vx); stream.SendNext(vy); stream.SendNext(vz);
        //         (vx, vy, vz) = Protcol.PositionSerialize((p2 - p1) / elapsed_time);
        //         stream.SendNext(vx); stream.SendNext(vy); stream.SendNext(vz);
        //         stream.SendNext(Protcol.RotationSerialize(my_transform.rotation));
        //     } else {
        //         // receive
        //
        //         // https://zenn.dev/o8que/books/bdcb9af27bdd7d/viewer/1ea062
        //         short px = (short) stream.ReceiveNext();
        //         short py = (short) stream.ReceiveNext();
        //         short pz = (short) stream.ReceiveNext();
        //         Vector3 net_pos = Protcol.PositionDeserialise(px, py, pz);
        //         px = (short) stream.ReceiveNext();
        //         py = (short) stream.ReceiveNext();
        //         pz = (short) stream.ReceiveNext();
        //         Vector3 net_vel = Protcol.PositionDeserialise(px, py, pz);
        //
        //         my_transform.rotation = Protcol.RotationDeserialize((byte) stream.ReceiveNext());
        //         float lag = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - info.SentServerTimestamp) / 1000f);
        //
        //         p1 = my_transform.position;
        //         p2 = net_pos + net_vel * lag;
        //         v1 = v2;
        //         v2 = net_vel * InterpolationPeriod;
        //         elapsed_time = (half) 0;
        //     }
        // }

        private void InitAnim()
        {
            animator = GetComponent<Animator>();
            _animationInfos = new Dictionary<CharacterState, AnimationInfo>()
            {
                {CharacterState.Idle, new AnimationInfo(AIdle, animator)},
                {CharacterState.Run, new AnimationInfo(ARun, animator)},
                {CharacterState.Damage, new AnimationInfo(ADamage, animator)},
                {CharacterState.Dead, new AnimationInfo(ADead, animator)},
                {CharacterState.Skill1, new AnimationInfo(ASkill1, animator)},
                {CharacterState.Skill2, new AnimationInfo(ASkill2, animator)},
                {CharacterState.Special, new AnimationInfo(ASpecial, animator)},
            };
        }

        private void InitReactiveEvent()
        {
            
        }

        #region InputCallback

        public void OnMove(InputAction.CallbackContext context)
        {
            if (!photonView.IsMine) return;
            var value = context.ReadValue<Vector2>();

            // Debug.Log(nameof(OnMove) + " is already set on callback");

            // FIXME
            if (value == Vector2.zero)
            {
                SetState(CharacterState.Idle);
            }
            else
            {
                SetState(CharacterState.Run);
            }

            _moveClass.Move(value);
        }


        public void OnSkill1(InputAction.CallbackContext context)
        {
            if (!photonView.IsMine) return;
            // Debug.Log(nameof(OnSkill1) + " is already set on callback");

            if (!CharacterStatus.UseSkill1()) return;

            Skill1();
        }

        public void OnSkill2(InputAction.CallbackContext context)
        {
            if (!photonView.IsMine) return;
            // Debug.Log(nameof(OnSkill2) + " is already set on callback");

            if (!CharacterStatus.UseSkill2()) return;

            Skill2();
        }

        public void OnSpecial(InputAction.CallbackContext context)
        {
            if (!photonView.IsMine) return;
            // Debug.Log(nameof(OnSpecial) + " is already set on callback");

            if (CurrentState != CharacterState.Idle) return; 
            if (!CharacterStatus.UseSpecial()) return;

            Special();
        }

        #endregion
    }


}
