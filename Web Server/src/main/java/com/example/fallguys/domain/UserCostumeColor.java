package com.example.fallguys.domain;

import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.*;
import org.hibernate.annotations.ColumnDefault;

import jakarta.persistence.*;

@Tag(name = "코스튬 색상 정보", description = "코스튬 색상 정보를 가진 Class")
@Entity(name = "UserCostumeColor")
@Getter
@Setter
@NoArgsConstructor
@Table(name = "UserCostumeColor")
public class UserCostumeColor {

    @Schema(name = "식별자")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "user_costume_color_number")
    private Long userCostumeColorNumber;

    @Schema(name = "user")
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "user_number", foreignKey = @ForeignKey(name = "FK_user"))
    private User user;

    @Schema(name = "CostumeColor")
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "costume_color_number", foreignKey = @ForeignKey(name = "FK_costume_color"))
    private CostumeColor costumeColor;

    @Schema(name = "색상 소유 여부")
    @Column(name = "user_costume_color_is_own", nullable = false)
    @ColumnDefault("false")
    private boolean isOwn;


    @Builder
    public UserCostumeColor (User user, CostumeColor costumeColor, boolean isOwn) {
        this.user = user;
        this.costumeColor = costumeColor;
        this.isOwn = isOwn;
    }
}
