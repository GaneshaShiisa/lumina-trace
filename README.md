# Lumina Trace

![Lumina Trace Banner](https://via.placeholder.com/800x400?text=Lumina+Trace) <!-- ※完成後にスクリーンショットやGIF画像に差し替えてください -->

## 概要 (Overview)

`Lumina Trace` は、3D空間内を浮游する光体コア（彗星）をコントロールし、空間に残る美しい光のリボン（Trail）や宇宙塵（Dust Particles）、拡散する衝撃波（Burst Particles）などのインタラクティブな描画表現を楽しむ作品です。

[![Play Lumina Trace](https://img.shields.io/badge/Play-Lumina%20Trace-brightgreen?style=for-the-badge&logo=unity)](https://ganeshashiisa.github.io/lumina-trace/)

## 主な機能 (Features)

- **光体コアの移動と物理演算**: Rigidbodyを用いた力学制御により、滑らかで重厚感のある曲線軌道を描きます。
- **リボン状の軌跡エフェクト (Trail Effect)**: 光体コアの移動に伴い、幅や透過度が変化する美しいグラデーション軌跡を描画します。
- **散乱粒子エフェクト (Particle Effects)**:
  - **Dust Particles**: 空間を漂う星屑および移動中に後方へ放出される微粒子。
  - **Burst Particles**: 方向転換・衝動発生時に放射状に拡散する衝撃波粒子。
- **カメラ追従と演出 (Cinemachine & Post-Processing)**:
  - Cinemachineによる滑らかな被写体追従。
  - URP Bloom / Depth of Field / Vignette を活用した奥行き感と光の溢れ出し表現。

## 操作方法 (Controls)

> **Note**: 現在開発中につき、一部の操作やキーアサインは変更される可能性があります。

| 操作                   | 割り当て                  | 説明                                                             |
| :--------------------- | :------------------------ | :--------------------------------------------------------------- |
| **方向転換 / 加速**    | マウス左クリック / タップ | 光体コアにランダムな方向への新たな力を加え、方向転換します。     |
| **カラーパレット変更** | _(未実装)_                | カラーテーマ（コズミック、オーロラ、サイバー等）を切り替えます。 |
| **UI表示切替**         | _(未実装)_                | パラメータ調整用GUIの表示/非表示を切り替えます。                 |

## プロジェクトのセットアップ (Getting Started)

1. **リポジトリのクローン**:
   ```bash
   git clone https://github.com/GaneshaShiisa/lumina-trace.git
   ```
2. **Unity Hub で開く**:
   - Unity Hub を起動し、`Unity 6` (または対象のバージョン) でプロジェクトを追加して開きます。
3. **メインシーンの移動**:
   - `Assets/Scenes/` 以下のメインシーン（例: `SampleScene`）を開いて再生（Play）します。

## ライセンス (License)

[MIT License](LICENSE)

## 作者 (Author)

- **GaneshaShiisa** - [GitHub Profile](https://github.com/GaneshaShiisa)
