package com.example.fallguys.domain;

import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.*;
import org.hibernate.annotations.ColumnDefault;

import jakarta.persistence.*;

@Tag(name = "코스튬 색상 정보", description = "코스튬 색상 정보를 가진 Class")
@Entity(name = "CostumeColor")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "costume_color")
public class CostumeColor {

    @Schema(name = "식별자")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "costume_color_number")
    private Long costumeColorNumber;

    @Schema(name = "색상 이름")
    @Column(name = "color_name", nullable = false,  length = 20)
    private String colorname;
}
