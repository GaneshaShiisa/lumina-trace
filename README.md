# Lumina Trace

<p align="center">
  <img src=".github/images/lumina-trace.png" width="800">
</p>

[![Play Lumina Trace](https://img.shields.io/badge/Play-Lumina%20Trace-brightgreen?style=for-the-badge&logo=unity)](https://ganesha-shiisa.github.io/lumina-trace/)

`Lumina Trace` は、3D空間内を浮遊する彗星のような光体コアをコントロールし、 Trail Renderer による光の軌跡や、 Particle System による残光演出を楽しむ ビジュアルインタラクション作品です。

## Play Now

👉 https://ganesha-shiisa.github.io/lumina-trace/

## 主な機能 (Features)

- **彗星風トレイル演出**:
  - Unity Particle System を活用した発光トレイルエフェクトを実装。
  - オブジェクト後方へ粒子を継続的に放出し、彗星や流星を思わせる残光表現を実現。
  - 速度に応じて尾の流れを変化させ、ダイナミックな飛行演出を構築。
- **リボン状の軌跡エフェクト (Trail Renderer)**:
  - 光体コアの移動履歴を Trail Renderer により描画し、空間上に美しい軌跡を残します。
- **光体コアの移動制御**:
  - Rigidbodyベースの移動システムにより、滑らかで重厚感のある曲線軌道を描きます。
- **カメラ追従と演出**:
  - 移動速度を考慮したダイナミックな追従カメラを実装。
  - 慣性を持つスムーズな位置補間により、ドローンカメラのような自然な追尾表現を実現。
  - 滑らかな視線補間によって、被写体を追いかける臨場感のあるカメラワークを構築。

## 操作方法 (Controls)

| 操作                | 割り当て                  | 説明                                                                    |
| :------------------ | :------------------------ | :---------------------------------------------------------------------- |
| **方向転換 / 加速** | マウス左クリック / タップ | 光体コアへ新たな推進力を与え、 ダイナミックな軌跡の変化を生み出します。 |

## プロジェクトのセットアップ (Getting Started)

1. **リポジトリのクローン**:
   ```bash
   git clone https://github.com/ganesha-shiisa/lumina-trace.git
   ```
2. **Unity Hub で開く**:
   - Unity Hub を起動し、`Unity 6` (または対象のバージョン) でプロジェクトを追加して開きます。
3. **メインシーンの移動**:
   - `Assets/Scenes/` 以下のメインシーン（例: `SampleScene`）を開いて再生（Play）します。

## ライセンス (License)

[MIT License](LICENSE)

## 作者 (Author)

- **GaneshaShiisa** - [GitHub Profile](https://github.com/ganesha-shiisa)
