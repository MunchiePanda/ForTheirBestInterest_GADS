# For Their Best Interest – Tech Demo README

## 🎮 Game Overview
**For Their Best Interest** is a narrative-driven bureaucratic puzzle game inspired by *Papers, Please*, set in Johannesburg, South Africa. The player acts as a caseworker at Abba Adoptions, a non-profit focused on ethical adoptions. The game aims to raise awareness of the systemic and emotional challenges of adoption through meaningful decision-making and immersive mechanics.

---

## 🧩 Gameplay Summary
- **Genre:** Narrative puzzle / simulation
- **Platform:** Unity 2D, Mobile (Android/iOS)
- **Target Audience:** Young South Africans (ages 18–30)
- **Game Loop:**
  1. Receive and review adoption cases
  2. Verify documents for legal compliance
  3. Conduct interactive interviews
  4. Match children with suitable families
  5. Make high-impact decisions and receive long-term feedback

---

## 🗓️ Demo Structure
The demo includes 3 in-game "days" with increasing complexity:

### **Day 1: Tutorial Onboarding**
- Introduces the player to document review and interviews
- One simple, emotional case: a grandmother applying to adopt her granddaughter

### **Day 2: Ethical Conflict**
- Introduces morally complex decisions
- Case: wealth vs. compassion dilemma (young couple vs. single dad)

### **Day 3: High-Stakes Emergency**
- Emergency walk-in case with siblings
- Introduces time pressure and emotional consequences

### **End Screen**
- Displays Abba Adoptions' mission
- Presents a QR code linking to their official page
- Offers options to replay or learn more

---

## 🧠 Key Mechanics
- **Document Verification System:** Checks legal documentation (medical, home study, income)
- **Interview System:** Dialogue trees reveal motivations, flagged responses
- **Matching Algorithm (Planned):** Pair children with applicants based on compatibility
- **Ethical Decision System:** Player must weigh legal vs. emotional outcomes
- **Feedback Loop:** Players receive delayed feedback on previous placements

---

## 🖥️ UI Elements
- Name, Income, Notes display
- Buttons: Check Docs, Interview, Approve, Reject
- Optional: Day counter, feedback panel, ethical decision meter

---

## 🎨 Art & Sound Style
- **Visual Style:** Pixel art with muted office backgrounds and expressive character sprites
- **Audio:** Ambient office sounds (typing, paper), emotional lo-fi African music

---

## 🎯 Educational Goal
The game is designed to:
- Raise awareness of the bureaucratic barriers to adoption in SA
- Build empathy by simulating difficult choices
- Encourage players to engage with or support Abba Adoptions

---

## 📍Platform Justification
Unity 2022.3 (2D)
- Mobile-first experience due to 99% mobile penetration in SA youth
- Easily expandable to desktop

---

## 🧪 Development Notes
- Code structure uses singletons (`GameManager`, `CaseManager`, `UIManager`)
- Data is currently hardcoded for simplicity; planned shift to JSON or ScriptableObjects
- Matching logic and visual outcomes are planned for future iterations

---

## 🔚 End Screen Content
- Mission summary of Abba Adoptions
- Stats about the orphan crisis in South Africa
- QR code to: [https://www.abbaadoptions.co.za](https://www.abbaadoptions.co.za)

---

## 📁 File Locations
- Scripts: `/Scripts/Managers`, `/Scripts/UI`, `/Scripts/Systems`
- UI: `/Canvas` in scene `MainDemoScene`
- End scene: `EndScene.unity`

---

## ✅ Planned Additions
- Child-family compatibility system
- Randomized case loading
- Post-decision letters/emails from children
- Time-pressure mechanic for emergency cases
- Full UI polish and animations
