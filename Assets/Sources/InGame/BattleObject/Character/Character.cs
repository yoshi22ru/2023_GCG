using System;
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

        // Animation Hash IDs
        private int _idleId;
        private int _runId;
        private int _damageId;
        private int _skill1Id;
        private int _skill2Id;
        private int _deadId;
        private int _specialId;

        // Length of Animations
        private float _idleLen;
        private float _runLen;
        private float _damageLen;
        private float _skill1Len;
        private float _skill2Len;
        private float _specialLen;
        private float _deadLen;

        #endregion

        protected enum CharacterState
        {
            None,
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
            Debug.Log(nameof(OnHitMyTeamObject));
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
            Debug.Log(nameof(OnHitEnemyTeamObject));
            var skillManager = battleObject as SkillManager;
            if (skillManager == null) return;

            if (skillManager.type == SkillManager.SkillType.Damage)
            {
                Debug.Log("Damage Hit\n" +
                          Utils.FormatBattleObjectInformation(gameObject, this) +
                          $"Value : {skillManager.GetSkillDamage}\n" +
                          $"");
                CharacterStatus.SetHP(CharacterStatus.CurrentHP - skillManager.GetSkillDamage);
            }

            SetState(CharacterState.Damage);
        }



        private void FixedUpdate()
        {
            if (GameManager.manager.current_state != GameManager.BattleState.Battle)
            {
                return;
            }


            if (photonView.IsMine)
            {
                p1 = p2;
                p2 = myTransform.position;
                _elapsedTime = (half)Time.deltaTime;
            }
            else
            {
                _elapsedTime += (half)Time.deltaTime;
                if (_elapsedTime < InterpolationPeriod)
                {
                    myTransform.position =
                        HermiteSpline.Interpolate(p1, p2, v1, v2, _elapsedTime / InterpolationPeriod);
                }
                else
                {
                    myTransform.position = Vector3.LerpUnclamped(p1, p2, _elapsedTime / InterpolationPeriod);
                }

                return;
            }

            if (CharacterStatus.IsDead)
            {
                if (CurrentState != CharacterState.Dead)
                {
                    SetState(CharacterState.Dead);
                }

                CharacterStatus.SetHP(CharacterStatus.MaxHP);
            }

            if (CharacterStatus.IsDead == false && CountDown.instance.isCountFinish == true)
            {
                // FIXME
                // else if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.K))
                // {
                //     characterStatus.SetIsDead(true);
                // }
            }
        }

        protected void SetState(CharacterState newState)
        {
            CurrentState = newState;

            animator.ResetTrigger(_idleId);
            animator.ResetTrigger(_runId);
            animator.ResetTrigger(_damageId);
            animator.ResetTrigger(_deadId);
            animator.ResetTrigger(_skill1Id);
            animator.ResetTrigger(_skill2Id);
            animator.ResetTrigger(_specialId);

            switch (newState)
            {
                case CharacterState.Idle:
                    animator.SetTrigger(_idleId);
                    break;
                case CharacterState.Run:
                    animator.SetTrigger(_runId);
                    break;
                case CharacterState.Damage:
                    animator.SetTrigger(_damageId);
                    break;
                case CharacterState.Dead:
                    animator.SetTrigger(_deadId);
                    break;
                case CharacterState.Skill1:
                    animator.SetTrigger(_skill1Id);
                    break;
                case CharacterState.Skill2:
                    animator.SetTrigger(_skill2Id);
                    break;
                case CharacterState.Special:
                    animator.SetTrigger(_specialId);
                    break;
            }
        }

        private async void DelaySetState(CharacterState state, float length)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(length));

            SetState(state);
        }

        protected virtual void Skill1()
        {
            // characterStatus.UseSkill1();
            // SetState(Character_State.Skill1);
            // var buf = Instantiate(skill1, skill1Point.position, transform.rotation);
            // if (is_child_1)
            // {
            //     buf.transform.parent = myTransform;
            // }
            //
            // _audioSourceCache.PlayOneShot(skill1SE);
        }

        protected virtual void Skill2()
        {
            // characterStatus.UseSkill2();
            // SetState(Character_State.Skill2);
            // var buf = Instantiate(skill2, skill2Point.position, transform.rotation);
            // if (is_child_2)
            // {
            //     buf.transform.parent = myTransform;
            // }
            //
            // _audioSourceCache.PlayOneShot(skill2SE);
        }

        protected virtual void Special()
        {
            // characterStatus.UseSpecial();
            // SetState(Character_State.Special);
            // var buf = Instantiate(special, specialPoint.position, transform.rotation);
            // if (is_child_Special)
            // {
            //     buf.transform.parent = myTransform;
            // }
            //
            // _audioSourceCache.PlayOneShot(specialSE);
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

            _idleId = Animator.StringToHash(AIdle);
            _runId = Animator.StringToHash(ARun);
            _damageId = Animator.StringToHash(ADamage);
            _deadId = Animator.StringToHash(ADead);
            _skill1Id = Animator.StringToHash(ASkill1);
            _skill2Id = Animator.StringToHash(ASkill2);
            _specialId = Animator.StringToHash(ASpecial);

            _idleLen = Utils.GetAnimationClipLength(animator.runtimeAnimatorController.animationClips, AIdle);
            _runLen = Utils.GetAnimationClipLength(animator.runtimeAnimatorController.animationClips, ARun);
            _damageLen = Utils.GetAnimationClipLength(animator.runtimeAnimatorController.animationClips, ADamage);
            _deadLen = Utils.GetAnimationClipLength(animator.runtimeAnimatorController.animationClips, ADead);
            _skill1Len = Utils.GetAnimationClipLength(animator.runtimeAnimatorController.animationClips, ASkill1);
            _skill2Len = Utils.GetAnimationClipLength(animator.runtimeAnimatorController.animationClips, ASkill2);
            _specialLen = Utils.GetAnimationClipLength(animator.runtimeAnimatorController.animationClips, ASpecial);
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

            // FIXME

            Skill1();
            DelaySetState(CharacterState.Idle, _skill1Len);
        }

        public void OnSkill2(InputAction.CallbackContext context)
        {
            if (!photonView.IsMine) return;
            // Debug.Log(nameof(OnSkill2) + " is already set on callback");

            if (!CharacterStatus.UseSkill2()) return;

            // FIXME

            Skill2();
            DelaySetState(CharacterState.Idle, _skill2Len);

        }

        public void OnSpecial(InputAction.CallbackContext context)
        {
            if (!photonView.IsMine) return;
            // Debug.Log(nameof(OnSpecial) + " is already set on callback");

            if (CurrentState != CharacterState.Idle) return; 
            if (!CharacterStatus.UseSpecial()) return;

            // FIXME

            Special();
            DelaySetState(CharacterState.Idle, _specialLen);
        }

        #endregion
    }


}
