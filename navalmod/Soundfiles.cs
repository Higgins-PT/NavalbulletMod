using System;
using Modding;
using UnityEngine;

namespace Navalmod
{
	// Token: 0x0200001C RID: 28
	public static class Soundfiles
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00006804 File Offset: 0x00004A04
		static Soundfiles()
		{
			Soundfiles.shootbig = new AudioClip[]//attack_big
			{
				Soundfiles.csb1,
				Soundfiles.csb2,
				Soundfiles.csb3,
				Soundfiles.csb4,
				Soundfiles.csb5,
				Soundfiles.csb6,
				Soundfiles.csb7,
				Soundfiles.csb8
			};
			Soundfiles.shootmid = new AudioClip[]//attack_mid
			{
				Soundfiles.csm1,
				Soundfiles.csm2,
				Soundfiles.csm3,
				Soundfiles.csm4
			};
			Soundfiles.shootsmall = new AudioClip[]//attack_small
			{
				Soundfiles.css1,
				Soundfiles.css2,
				Soundfiles.css3,
				Soundfiles.css4,
				Soundfiles.css5,
				Soundfiles.css6
			};
			Soundfiles.shootexplosion = new AudioClip[]//explosion_attack
			{
				Soundfiles.csex1,
				Soundfiles.csex2,
				Soundfiles.csex3,
				Soundfiles.csex4,
				Soundfiles.csex5,
				Soundfiles.csex6,
				Soundfiles.csex7,
				Soundfiles.csex8,
				Soundfiles.csex9,
				Soundfiles.csex10,
				Soundfiles.csex11,
				Soundfiles.csex12
			};
			Soundfiles.explosionmid = new AudioClip[]//attack_bass_add
			{
				Soundfiles.exm1,
				Soundfiles.exm2,
				Soundfiles.exm3,
				Soundfiles.exm4
			};
			Soundfiles.explosionmidfar = new AudioClip[]//body_mid_far
			{
				Soundfiles.exmf1,
				Soundfiles.exmf2,
				Soundfiles.exmf3,
				Soundfiles.exmf4
			};
			Soundfiles.explosionbig = new AudioClip[]//body_big
			{
				Soundfiles.exb1,
				Soundfiles.exb2,
				Soundfiles.exb3,
				Soundfiles.exb4
			};
			Soundfiles.explosionbigfar = new AudioClip[]//body_big_far
			{
				Soundfiles.exbf1,
				Soundfiles.exbf2,
				Soundfiles.exbf3,
				Soundfiles.exbf4
			};
			Soundfiles.explosionbigfar2 = new AudioClip[]//explosion_fast_big
			{
				Soundfiles.exbf2_1,
				Soundfiles.exbf2_2,
				Soundfiles.exbf2_3,
				Soundfiles.exbf2_4
			};
			Soundfiles.explosionsmall = new AudioClip[]//attack_small
			{
				Soundfiles.exs1,
				Soundfiles.exs2,
				Soundfiles.exs3,
				Soundfiles.exs4,
				Soundfiles.exs5
			};
		}

		// Token: 0x040000B0 RID: 176
		public static AudioClip[] shootbig;

		// Token: 0x040000B1 RID: 177
		public static AudioClip csb1 = ModResource.GetAudioClip("firebig_1");

		// Token: 0x040000B2 RID: 178
		public static AudioClip csb2 = ModResource.GetAudioClip("firebig_2");

		// Token: 0x040000B3 RID: 179
		public static AudioClip csb3 = ModResource.GetAudioClip("firebig_3");

		// Token: 0x040000B4 RID: 180
		public static AudioClip csb4 = ModResource.GetAudioClip("firebig_4");

		// Token: 0x040000B5 RID: 181
		public static AudioClip csb5 = ModResource.GetAudioClip("firebig_5");

		// Token: 0x040000B6 RID: 182
		public static AudioClip csb6 = ModResource.GetAudioClip("firebig_6");

		// Token: 0x040000B7 RID: 183
		public static AudioClip csb7 = ModResource.GetAudioClip("firebig_7");

		// Token: 0x040000B8 RID: 184
		public static AudioClip csb8 = ModResource.GetAudioClip("firebig_8");

		// Token: 0x040000B9 RID: 185
		public static AudioClip csm1 = ModResource.GetAudioClip("firemid_1");

		// Token: 0x040000BA RID: 186
		public static AudioClip csm2 = ModResource.GetAudioClip("firemid_2");

		// Token: 0x040000BB RID: 187
		public static AudioClip csm3 = ModResource.GetAudioClip("firemid_3");

		// Token: 0x040000BC RID: 188
		public static AudioClip csm4 = ModResource.GetAudioClip("firemid_4");

		// Token: 0x040000BD RID: 189
		public static AudioClip css1 = ModResource.GetAudioClip("firesmall_1");

		// Token: 0x040000BE RID: 190
		public static AudioClip css2 = ModResource.GetAudioClip("firesmall_2");

		// Token: 0x040000BF RID: 191
		public static AudioClip css3 = ModResource.GetAudioClip("firesmall_3");

		// Token: 0x040000C0 RID: 192
		public static AudioClip css4 = ModResource.GetAudioClip("firesmall_4");

		// Token: 0x040000C1 RID: 193
		public static AudioClip css5 = ModResource.GetAudioClip("firesmall_5");

		// Token: 0x040000C2 RID: 194
		public static AudioClip css6 = ModResource.GetAudioClip("firesmall_6");

		// Token: 0x040000C3 RID: 195
		public static AudioClip[] shootmid;

		// Token: 0x040000C4 RID: 196
		public static AudioClip[] shootsmall;

		// Token: 0x040000C5 RID: 197
		public static AudioClip csex1 = ModResource.GetAudioClip("firesex_1");

		// Token: 0x040000C6 RID: 198
		public static AudioClip csex2 = ModResource.GetAudioClip("firesex_2");

		// Token: 0x040000C7 RID: 199
		public static AudioClip csex3 = ModResource.GetAudioClip("firesex_3");

		// Token: 0x040000C8 RID: 200
		public static AudioClip csex4 = ModResource.GetAudioClip("firesex_4");

		// Token: 0x040000C9 RID: 201
		public static AudioClip csex5 = ModResource.GetAudioClip("firesex_5");

		// Token: 0x040000CA RID: 202
		public static AudioClip csex6 = ModResource.GetAudioClip("firesex_6");

		// Token: 0x040000CB RID: 203
		public static AudioClip csex7 = ModResource.GetAudioClip("firesex_7");

		// Token: 0x040000CC RID: 204
		public static AudioClip csex8 = ModResource.GetAudioClip("firesex_8");

		// Token: 0x040000CD RID: 205
		public static AudioClip csex9 = ModResource.GetAudioClip("firesex_9");

		// Token: 0x040000CE RID: 206
		public static AudioClip csex10 = ModResource.GetAudioClip("firesex_10");

		// Token: 0x040000CF RID: 207
		public static AudioClip csex11 = ModResource.GetAudioClip("firesex_11");

		// Token: 0x040000D0 RID: 208
		public static AudioClip csex12 = ModResource.GetAudioClip("firesex_12");

		// Token: 0x040000D1 RID: 209
		public static AudioClip exm1 = ModResource.GetAudioClip("exm_1");

		// Token: 0x040000D2 RID: 210
		public static AudioClip exm2 = ModResource.GetAudioClip("exm_2");

		// Token: 0x040000D3 RID: 211
		public static AudioClip exm3 = ModResource.GetAudioClip("exm_3");

		// Token: 0x040000D4 RID: 212
		public static AudioClip exm4 = ModResource.GetAudioClip("exm_4");

		// Token: 0x040000D5 RID: 213
		public static AudioClip exs1 = ModResource.GetAudioClip("exs_1");

		// Token: 0x040000D6 RID: 214
		public static AudioClip exs2 = ModResource.GetAudioClip("exs_2");

		// Token: 0x040000D7 RID: 215
		public static AudioClip exs3 = ModResource.GetAudioClip("exs_3");

		// Token: 0x040000D8 RID: 216
		public static AudioClip exs4 = ModResource.GetAudioClip("exs_4");

		// Token: 0x040000D9 RID: 217
		public static AudioClip exs5 = ModResource.GetAudioClip("exs_5");

		// Token: 0x040000DA RID: 218
		public static AudioClip exb1 = ModResource.GetAudioClip("exbig_1");

		// Token: 0x040000DB RID: 219
		public static AudioClip exb2 = ModResource.GetAudioClip("exbig_2");

		// Token: 0x040000DC RID: 220
		public static AudioClip exb3 = ModResource.GetAudioClip("exbig_3");

		// Token: 0x040000DD RID: 221
		public static AudioClip exb4 = ModResource.GetAudioClip("exbig_4");

		// Token: 0x040000DE RID: 222
		public static AudioClip exbf1 = ModResource.GetAudioClip("exbigf_1");

		// Token: 0x040000DF RID: 223
		public static AudioClip exbf2 = ModResource.GetAudioClip("exbigf_2");

		// Token: 0x040000E0 RID: 224
		public static AudioClip exbf3 = ModResource.GetAudioClip("exbigf_3");

		// Token: 0x040000E1 RID: 225
		public static AudioClip exbf4 = ModResource.GetAudioClip("exbigf_4");

		// Token: 0x040000E2 RID: 226
		public static AudioClip exmf1 = ModResource.GetAudioClip("exmf_1");

		// Token: 0x040000E3 RID: 227
		public static AudioClip exmf2 = ModResource.GetAudioClip("exmf_2");

		// Token: 0x040000E4 RID: 228
		public static AudioClip exmf3 = ModResource.GetAudioClip("exmf_3");

		// Token: 0x040000E5 RID: 229
		public static AudioClip exmf4 = ModResource.GetAudioClip("exmf_4");

		// Token: 0x040000E6 RID: 230
		public static AudioClip exbf2_1 = ModResource.GetAudioClip("exbigf2_1");

		// Token: 0x040000E7 RID: 231
		public static AudioClip exbf2_2 = ModResource.GetAudioClip("exbigf2_2");

		// Token: 0x040000E8 RID: 232
		public static AudioClip exbf2_3 = ModResource.GetAudioClip("exbigf2_3");

		// Token: 0x040000E9 RID: 233
		public static AudioClip exbf2_4 = ModResource.GetAudioClip("exbigf2_4");

        public static AudioClip gunfire = ModResource.GetAudioClip("gunfire");

        // Token: 0x040000EA RID: 234
        public static AudioClip[] shootexplosion;

		// Token: 0x040000EB RID: 235
		public static AudioClip[] explosionmid;

		// Token: 0x040000EC RID: 236
		public static AudioClip[] explosionmidfar;

		// Token: 0x040000ED RID: 237
		public static AudioClip[] explosionbigfar;

		// Token: 0x040000EE RID: 238
		public static AudioClip[] explosionbigfar2;

		// Token: 0x040000EF RID: 239
		public static AudioClip[] explosionbig;

		// Token: 0x040000F0 RID: 240
		public static AudioClip[] explosionsmall;
	}
}
